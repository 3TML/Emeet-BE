using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.Authentication
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public bool IsExpert { get; set; }
        public List<Guid>? ListCategoryId { get; set; }
        public string? Experience { get; set; }
        public decimal? PricePerMinute { get; set; }
    }
}
