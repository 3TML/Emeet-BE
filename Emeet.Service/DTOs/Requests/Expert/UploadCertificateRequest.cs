using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.Expert
{
    public class UploadCertificateRequest
    {
        public Guid ExpertId { get; set; }
        public required IList<IFormFile> Certificates { get; set; }
    }
}
