using Emeet.Domain.Specifications;
using Emeet.Service.DTOs.Requests.User;
using Emeet.Service.DTOs.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Interfaces
{
    public interface IUserService
    {
        Task<IPaginate<GetUserResponse>> GetAllUser(string fullName, string role, int page, int size);
        Task<bool> UpdateProfileById(Guid id, UpdateUserRequest updateUserRequest);
    }
}
