using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public DateTime DatePaid { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Status { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
