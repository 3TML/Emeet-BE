using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Enums;
using Emeet.Domain.Exceptions;
using Emeet.Domain.Interfaces;
using Emeet.Service.DTOs.Requests.Authentication;
using Emeet.Service.DTOs.Responses.Authentication;
using Emeet.Service.Helpers;
using Emeet.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Task<FetchUserResponse> FetchUser(string accessToken)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> LoginPassword(LoginPasswordRequest request)
        {
            var account = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Username.Equals(request.Username) && u.Password.Equals(request.Password));
            // return null if user not found
            if (account == null)
            {
                return null;
            }
            
            // authentication successful so generate jwt token and refresh token
            var accessToken = JWTHelper.GenerateToken(account.Username, account.Role!, _configuration["JWTSettings:Key"]!, _configuration["JWTSettings:Issuer"]!, _configuration["JWTSettings:Audience"]!);
            var refreshToken = JWTHelper.GenerateRefreshToken();
            account.RefreshToken = refreshToken;
            account.AccessToken = accessToken;
            account.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
            _unitOfWork.GetRepository<User>().UpdateAsync(account);

            var response = _mapper.Map<LoginResponse>(account);

            var expert = await _unitOfWork.GetRepository<Expert>().SingleOrDefaultAsync(predicate: x => x.UserId.Equals(account.Id));

            if (expert != null)
            {
                response.ExpertInformation = _mapper.Map<ExpertInformation>(expert);
            }
            return response;
        }

        public Task<bool> Logout(LogoutRequest logoutRequest)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterUser(RegisterRequest request)
        {
            var userName = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x=>x.Username.Equals(request.Username));
            if (userName != null)
            {
                throw new Exception("Username đã tồn tại");
            }
            var account = _mapper.Map<User>(request);
            account.Id = Guid.NewGuid();
            account.Avatar = _configuration["Default:Avatar_Default"]??"";
            account.Bio = "";
            account.DateCreate = DateTime.UtcNow;
            account.Email = request.Username;
            account.Status = UserStatus.Active;
            account.IsExpert = false;
            if (request.IsExpert != null && request.IsExpert == true)
            {
                account.IsExpert = true;
                var expert = _mapper.Map<Expert>(request);
                expert.Id = Guid.NewGuid();
                expert.UserId = account.Id;
                expert.TotalPreview = 0;
                expert.Rate = 0;
                expert.TotalRate = 0;
                expert.Status = ExpertStatus.Active;
                await _unitOfWork.GetRepository<Expert>().InsertAsync(expert);

                if (request.ListCategoryId != null)
                {
                    foreach (var item in request.ListCategoryId)
                    {
                        var categoryExpert = new ExpertCategory()
                        {
                            Id = Guid.NewGuid(),
                            CategoryId = item,
                            ExpertId = expert.Id
                        };
                        await _unitOfWork.GetRepository<ExpertCategory>().InsertAsync(categoryExpert);
                    }
                }
            }
            await _unitOfWork.GetRepository<User>().InsertAsync(account);

            bool isInsert = await _unitOfWork.CommitAsync()>0;
            return isInsert;
        }
    }
}
