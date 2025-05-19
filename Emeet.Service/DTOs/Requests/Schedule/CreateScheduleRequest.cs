using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.Schedule
{
    public class CreateScheduleRequest
    {
        public Guid ExpertId { get; set; }
        public required IList<ScheduleInformation> Schedules {  get; set; }
    }

    public class ScheduleInformation
    {
        public string DayOfMonth { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Status { get; set; }
    }
}
