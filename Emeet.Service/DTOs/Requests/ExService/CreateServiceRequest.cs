using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.ExpertService
{
    public class CreateServiceRequest
    {
        public Guid ExpertId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Time { get; set; }
        public decimal Price { get; set; }
    }
}
