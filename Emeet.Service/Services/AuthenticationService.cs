using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Enums;
using Emeet.Domain.Interfaces;
using Emeet.Service.DTOs.Requests.Authentication;
using Emeet.Service.DTOs.Responses.Authentication;
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
