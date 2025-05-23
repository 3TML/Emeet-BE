using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Responses.Appointment
{
    public class GetAppointmentsResponse
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; }
        public string CustomerName { get; set; }
        public string ExpertName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string LinkMeet { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
