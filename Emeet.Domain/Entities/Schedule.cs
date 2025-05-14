using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Domain.Entities
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public Guid ExpertId { get; set; }
        public string DayOfMonth { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Status { get; set; }

        public virtual Expert Expert { get; set; }
    }
}
