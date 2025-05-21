using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.Appointment
{
    public class BookExperRequest
    {
        public Guid ExpertId { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
