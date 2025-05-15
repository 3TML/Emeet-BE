using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.Authentication
{
    public class LoginPasswordRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
