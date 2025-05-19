using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Responses.Schedule
{
    public class GetScheduleResponse
    {
        public Guid Id { get; set; }
        public string DayOfMonth { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public bool Status { get; set; }
    }
}
