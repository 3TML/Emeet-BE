using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.Expert
{
    public class DeleteCertificateRequest
    {
        public required IList<Guid> CerIds { get; set; }
    }
}
