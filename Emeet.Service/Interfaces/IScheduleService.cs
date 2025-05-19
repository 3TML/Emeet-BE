using Emeet.Service.DTOs.Requests.Schedule;
using Emeet.Service.DTOs.Responses.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Interfaces
{
    public interface IScheduleService
    {
        Task<bool> CreateSchedule(CreateScheduleRequest request);
        Task<IList<GetScheduleResponse>> GetScheduleByExpertId(Guid ExpertId);
        Task<bool> UpdateSchedule(Guid id, UpdateScheduleRequest request);
    }
}
