using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Enums;
using Emeet.Domain.Exceptions;
using Emeet.Domain.Interfaces;
using Emeet.Service.DTOs.Requests.Appointment;
using Emeet.Service.DTOs.Responses.Appointment;
using Emeet.Service.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private List<float> GenerateTimeSlot(float startTime, float endTime, DateTime date)
        {
            DateTime now = DateTime.Now;
            bool isToday = (date.Date == now.Date);

            double effectiveStart = startTime;

            if (isToday)
            {
                double nowHour = now.Hour + now.Minute / 60.0;
                effectiveStart = Math.Max(startTime, nowHour);
            }

            double roundedStart = Math.Ceiling(effectiveStart * 2) / 2.0;

            if (roundedStart >= endTime)
                return new List<float>();

            return Enumerable.Range(0, (int)((endTime - roundedStart) * 2) + 1)
                             .Select(i => (float)(roundedStart + i * 0.5))
                             .ToList();
        }

        public async Task<GetAvailableTimeResponse> GetAvailableTime(GetAvailableTimeRequest request)
        {
            var availableTime = new GetAvailableTimeResponse();
            var dayOfWeek = request.StartDate.DayOfWeek.ToString();

            var service = await _unitOfWork.GetRepository<ExService>().SingleOrDefaultAsync(predicate: x => x.Id == request.ServiceId);
            if (service == null)
            {
                throw new NotFoundException($"Service not found with id {request.ServiceId}");
            }

            var schedule = await _unitOfWork.GetRepository<Schedule>().SingleOrDefaultAsync(predicate: x => x.ExpertId == request.ExpertId && x.DayOfWeek.Equals(dayOfWeek));
            if (schedule == null)
            {
                throw new NotFoundException($"Schedule not found with expert id {request.ExpertId}");
            }

            var appointments = await _unitOfWork.GetRepository<Appointment>()
                                                .GetListAsync(
                                                    predicate: x => x.ExpertId == request.ExpertId && x.StartTime.Date == request.StartDate.Date && x.StartTime >= DateTime.Now
                                                );

            var listTimeSlot = GenerateTimeSlot((float)schedule.StartTime!.Value.TotalHours, (float)schedule.EndTime!.Value.TotalHours - (float)service.Time, request.StartDate);
            foreach (var appointment in appointments)
            {
                float startFloat = appointment.StartTime.Hour + appointment.StartTime.Minute / 60f;
                float endFloat = appointment.EndTime.Hour + appointment.EndTime.Minute / 60f;

                listTimeSlot.RemoveAll(t => t >= startFloat && t < endFloat);
            }

            availableTime.AvailableTime = listTimeSlot;
            return availableTime;
        }

        public async Task<bool> BookExpert(BookExperRequest request)
        {
            var expert = await _unitOfWork.GetRepository<Expert>().SingleOrDefaultAsync(predicate: x => x.Id == request.ExpertId);
            if (expert == null)
            {
                throw new NotFoundException($"Expert not found with id {request.ExpertId}");
            }
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id == request.UserId);
            if (user == null)
            {
                throw new NotFoundException($"User not found with id {request.UserId}");
            }
            var service = await _unitOfWork.GetRepository<ExService>().SingleOrDefaultAsync(predicate: x => x.Id == request.ServiceId);
            if (service == null)
            {
                throw new NotFoundException($"Service not found with id {request.ServiceId}");
            }

            var appointment = _mapper.Map<Appointment>(request);
            appointment.Status = AppointmentStatus.Pending;
            appointment.LinkMeet = "";
            await _unitOfWork.GetRepository<Appointment>().InsertAsync(appointment);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<GetAppointmentsResponse> GetAppointmentByCustomerId(Guid customerId, DateTime? date, string expertName, int page, int size)
        {
            var predicate = PredicateBuilder.New<Appointment>(x => x.UserId == customerId && x.Expert.User.FullName.Contains(expertName));
            if (date != null)
            {
                predicate = predicate.And(x => x.StartTime.Date == date.Value.Date);
            }
            var appointments = await _unitOfWork.GetRepository<Appointment>().GetPagingListAsync(predicate: predicate, include: x=>x.Include(s=>s.Expert).ThenInclude(s=>s.User).Include(s=>s.ExService), page: page, size: size);
            return _mapper.Map<GetAppointmentsResponse>(appointments);
        }

        public async Task<GetAppointmentsResponse> GetAppointmentByExpertId(Guid expertId, DateTime? date, string customerName, int page, int size)
        {
            var predicate = PredicateBuilder.New<Appointment>(x => x.ExpertId == expertId && x.Expert.User.FullName.Contains(customerName));
            if (date != null)
            {
                predicate = predicate.And(x => x.StartTime.Date == date.Value.Date);
            }
            var appointments = await _unitOfWork.GetRepository<Appointment>().GetPagingListAsync(predicate: predicate, include: x => x.Include(s => s.Expert).ThenInclude(s => s.User).Include(s => s.ExService), page: page, size: size);
            return _mapper.Map<GetAppointmentsResponse>(appointments);
        }

        //public async Task<string> CreateLinkGgMeet()
        //{
        //    try
        //    {
        //        string[] Scopes = { CalendarService.Scope.Calendar };
        //        string serviceAccountEmail = "emeet-google-meet@emeet-460414.iam.gserviceaccount.com";
        //        string userEmailToDelegate = "hairhub.business@gmail.com"; // email người dùng bạn muốn tạo sự kiện trên calendar

        //        // Đường dẫn đến file key json tải từ Google Cloud Console
        //        string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //        string keyFilePath = Path.Combine(basePath, "Configs", "service-account-key.json");

        //        Console.WriteLine($"Path file JSON key: {keyFilePath}");
        //        if (!File.Exists(keyFilePath))
        //        {
        //            throw new FileNotFoundException("Không tìm thấy file key JSON Google Service Account", keyFilePath);
        //        }

        //        var credential = GoogleCredential.FromFile(keyFilePath)
        //                          .CreateScoped(Scopes)
        //        .CreateWithUser(userEmailToDelegate);




        //        var service = new CalendarService(new BaseClientService.Initializer()
        //        {
        //            HttpClientInitializer = credential,
        //            ApplicationName = "Google Meet .NET Demo",
        //        });

        //        var newEvent = new Event()
        //        {
        //            Summary = "Cuộc họp thử Google Meet",
        //            Start = new EventDateTime()
        //            {
        //                DateTime = DateTime.Now,
        //                TimeZone = "Asia/Ho_Chi_Minh"
        //            },
        //            End = new EventDateTime()
        //            {
        //                DateTime = DateTime.Now.AddHours(2),
        //                TimeZone = "Asia/Ho_Chi_Minh"
        //            },
        //            Attendees = new List<EventAttendee>()
        //{
        //    new EventAttendee() { Email = "tientranxuan263@gmail.com" },
        //    new EventAttendee() { Email = "tientxse161471@fpt.edu.vn" }
        //},
        //            ConferenceData = new ConferenceData()
        //            {
        //                CreateRequest = new CreateConferenceRequest()
        //                {
        //                    RequestId = Guid.NewGuid().ToString(),
        //                    ConferenceSolutionKey = new ConferenceSolutionKey()
        //                    {
        //                        Type = "hangoutsMeet"
        //                    }
        //                }
        //            }
        //        };

        //        var request = service.Events.Insert(newEvent, "primary");
        //        request.ConferenceDataVersion = 1;

        //        Event createdEvent = await request.ExecuteAsync();
        //        var meetLink = createdEvent.ConferenceData.EntryPoints?[0].Uri;

        //        return meetLink;
        //    }
        //    catch (Google.GoogleApiException ex)
        //    {
        //        Console.WriteLine($"Lỗi API Google: {ex.Message}");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Lỗi chung: {ex.Message}");
        //        throw;
        //    }

        //}
    }
}
