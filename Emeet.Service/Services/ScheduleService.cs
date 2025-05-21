using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Exceptions;
using Emeet.Domain.Interfaces;
using Emeet.Service.DTOs.Requests.Schedule;
using Emeet.Service.DTOs.Responses.Schedule;
using Emeet.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateSchedule(CreateScheduleRequest request)
        {
            var expert = await _unitOfWork.GetRepository<Expert>().SingleOrDefaultAsync(predicate: x=>x.Id == request.ExpertId);
            if (expert == null) 
            {
                throw new NotFoundException($"Expert not found with id {request.ExpertId}");
            }
            foreach (var schedule in request.Schedules)
            {
                var scheduleEntity = _mapper.Map<Schedule>(schedule);
                scheduleEntity.ExpertId = request.ExpertId;
                scheduleEntity.Id = Guid.NewGuid();
                await _unitOfWork.GetRepository<Schedule>().InsertAsync(scheduleEntity);
            }
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IList<GetScheduleResponse>> GetScheduleByExpertId(Guid expertId)
        {
            var schedules = await _unitOfWork.GetRepository<Schedule>().GetListAsync(predicate: x=>x.ExpertId == expertId);
            return _mapper.Map<IList<GetScheduleResponse>>(schedules);
        }

        public async Task<bool> UpdateSchedule(Guid id, UpdateScheduleRequest request)
        {
            var schedule = await _unitOfWork.GetRepository<Schedule>().SingleOrDefaultAsync(predicate: x => x.Id == id);
            schedule.DayOfWeek = request.DayOfWeek;
            schedule.Status = request.Status;
            schedule.StartTime = request.StartTime;
            schedule.EndTime = request.EndTime;
            _unitOfWork.GetRepository<Schedule>().UpdateAsync(schedule);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
