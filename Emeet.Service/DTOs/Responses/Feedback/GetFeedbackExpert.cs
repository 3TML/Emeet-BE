using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Responses.Feedback
{
    public class GetFeedbackExpert
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
