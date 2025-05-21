using Emeet.Service.DTOs.Requests.Appointment;
using Emeet.Service.DTOs.Responses.Appointment;
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
        //Task<string> CreateLinkGgMeet();
    }
}
