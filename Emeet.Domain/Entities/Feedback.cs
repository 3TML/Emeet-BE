using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Domain.Entities
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid ExpertId { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Expert Expert { get; set; }
    }
}
