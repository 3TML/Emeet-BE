using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Responses.Appointment
{
    public class GetAvailableTimeResponse
    {
        public IList<float> AvailableTime { get; set; } = new List<float>();
    }
}
