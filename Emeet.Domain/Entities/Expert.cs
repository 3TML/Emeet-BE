using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Domain.Entities
{
    public class Expert
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Experience { get; set; }
        public decimal PricePerMinute { get; set; }
        public int TotalReview { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalRate { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ExpertCategory> ExpertCategories { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<StaticFile> StaticFiles { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
