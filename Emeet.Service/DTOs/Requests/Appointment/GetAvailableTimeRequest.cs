using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.Appointment
{
    public class GetAvailableTimeRequest
    {
        public DateTime StartDate { get; set; }
        public Guid ServiceId { get; set; }
        public Guid ExpertId { get; set; }
    }
}
