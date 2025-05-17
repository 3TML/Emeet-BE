using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Responses.Expert
{
    public class GetExpertResponse
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public string? Bio { get; set; }
        public string Experience { get; set; }
        public decimal Rate { get; set; }
        public int TotalPreview { get; set; }
        public List<string> ListCategory { get; set; }
    }
}
