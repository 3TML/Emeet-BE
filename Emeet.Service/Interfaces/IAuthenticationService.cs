using Emeet.Service.DTOs.Requests.Authentication;
using Emeet.Service.DTOs.Responses.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<LoginResponse> Login(string userName, string password);
        public Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        public Task<bool> Logout(LogoutRequest logoutRequest);
        public Task<FetchUserResponse> FetchUser(string accessToken);
        public Task<bool> RegisterUser(RegisterRequest request);
    }
}
