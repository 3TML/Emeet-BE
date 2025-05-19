using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.Schedule
{
    public class UpdateScheduleRequest
    {
        public string DayOfMonth { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool Status { get; set; }
    }
}
