using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Responses.Authentication
{
    public class FetchUserResponse
    {
        public string Role { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string Gender { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public bool IsExpert { get; set; }

    }
}
