using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ExpertId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string LinkMeet { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
        public virtual Expert Expert { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
