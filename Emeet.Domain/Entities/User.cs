using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public string? Bio { get; set; }
        public string Gender { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public bool IsExpert { get; set; }

        public virtual ICollection<Expert> Experts { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
