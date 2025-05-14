using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.DTOs.Requests.Email
{
    public class CheckExistEmailResrequest
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
