using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Domain.Entities
{
    public class ExService
    {
        public Guid Id { get; set; }
        public Guid ExpertId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Time { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Expert Expert { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
