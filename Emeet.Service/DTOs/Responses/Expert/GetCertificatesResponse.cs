using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Responses.Expert
{
    public class GetCertificatesResponse
    {
        public Guid Id { get; set; }
        public Guid ExpertId { get; set; }
        public string Link { get; set; } = "";
        public string Type { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
