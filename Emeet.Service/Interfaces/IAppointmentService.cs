using Emeet.Service.DTOs.Requests.Appointment;
using Emeet.Service.DTOs.Responses.Appointment;
using Emeet.Service.DTOs.Responses.Expert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Interfaces
{
    public interface IAppointmentService
    {
        Task<GetAvailableTimeResponse> GetAvailableTime(GetAvailableTimeRequest request);
        Task<bool> BookExpert(BookExperRequest request);
        Task<GetAppointmentsResponse> GetAppointmentByCustomerId(Guid customerId, DateTime? date, string expertName, int page, int size);
        Task<GetAppointmentsResponse> GetAppointmentByExpertId(Guid expertId, DateTime? date, string customerName, int page, int size);
        //Task<string> CreateLinkGgMeet();
    }
}
