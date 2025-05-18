using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.User
{
    public class UpdateUserRequest
    {
        public string? FullName { get; set; }
        public IFormFile? Avatar { get; set; }
        public string? Bio { get; set; }
        public string? Gender { get; set; }
        public UpdateExpertRequest? UpdateExpertRequest { get; set; }
    }

    public class UpdateExpertRequest
    {
        public string? Experience { get; set; }
        public decimal? PricePerMinute { get; set; }
    }
}
