using Emeet.Service.DTOs.Requests.Authentication;
using Emeet.Service.DTOs.Responses.Authentication;
using Emeet.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<FetchUserResponse> FetchUser(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logout(LogoutRequest logoutRequest)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            throw new NotImplementedException();
        }
    }
}
