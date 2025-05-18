using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Enums;
using Emeet.Domain.Exceptions;
using Emeet.Domain.Interfaces;
using Emeet.Service.DTOs.Requests.Authentication;
using Emeet.Service.DTOs.Responses.Authentication;
using Emeet.Service.Helpers;
using Emeet.Service.Interfaces;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
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

        private string clientIdWeb = "190054359249-e9qgvopco4osh7qacqfk7pjiiuub3kr1.apps.googleusercontent.com";

        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<LoginResponse> FetchUser(string accessToken)
        {
            var account = await _unitOfWork.GetRepository<User>()
                .SingleOrDefaultAsync(predicate: x => x.AccessToken.Equals(accessToken) && x.RefreshTokenExpiry >= DateTime.Now);

            if (account == null)
            {
                throw new NotFoundException("Không tìm thấy access token!");
            }
            var response = _mapper.Map<LoginResponse>(account);

            var expert = await _unitOfWork.GetRepository<Expert>().SingleOrDefaultAsync(predicate: x => x.UserId.Equals(account.Id));

            if (expert != null)
            {
                response.ExpertInformation = _mapper.Map<ExpertInformation>(expert);
            }
            return response;
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
            bool isUpdate = await _unitOfWork.CommitAsync() > 0;
            if (!isUpdate)
            {
                throw new Exception("Login failed");
            }

            var response = _mapper.Map<LoginResponse>(account);

            var expert = await _unitOfWork.GetRepository<Expert>().SingleOrDefaultAsync(predicate: x => x.UserId.Equals(account.Id));

            if (expert != null)
            {
                response.ExpertInformation = _mapper.Map<ExpertInformation>(expert);
            }
            return response;
        }

        public async Task<bool> Logout(LogoutRequest logoutRequest)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.RefreshToken.Equals(logoutRequest.RefreshToken));
            if (user == null)
            {
                throw new NotFoundException("Refresh token not found!");
            }
            user.RefreshToken = "";
            _unitOfWork.GetRepository<User>().UpdateAsync(user);
            bool isDelete = await _unitOfWork.CommitAsync() > 0;
            return isDelete;
        }

        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var userEntity = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                                                            predicate: x => x.RefreshToken.Equals(refreshTokenRequest.RefreshToken)
                                                            && x.RefreshTokenExpiry >= DateTime.Now);
            if (userEntity == null)
            {
                throw new Exception("RefreshToken not found or expired");
            }

            userEntity.AccessToken = JWTHelper.GenerateToken(userEntity.Username, userEntity.Role!, _configuration["JWTSettings:Key"]!, _configuration["JWTSettings:Issuer"]!, _configuration["JWTSettings:Audience"]!);

            _unitOfWork.GetRepository<User>().UpdateAsync(userEntity);
            bool isUpdate = await _unitOfWork.CommitAsync() > 0;
            if (!isUpdate)
            {
                throw new Exception("Cannot insert new access token to DB");
            }
            return new RefreshTokenResponse() { AccessToken = userEntity.AccessToken, RefreshToken = userEntity.RefreshToken };
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
            account.FullName = request.FullName;
            if (request.IsExpert != null && request.IsExpert == true)
            {
                var expert = _mapper.Map<Expert>(request);
                expert.Id = Guid.NewGuid();
                expert.UserId = account.Id;
                expert.TotalReview = 0;
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

        public async Task<LoginResponse> LoginGoogle(LoginGoogleRequest checkLoginGoogle)
        {
            try
            {
                string clientId = clientIdWeb;
                
                // Xác thực token với Google
                var payload = await GoogleJsonWebSignature.ValidateAsync(checkLoginGoogle.IdToken, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { clientId }
                });

                if (payload == null)
                {
                    throw new Exception("Không có dữ liệu từ Google API");
                }
                // Lấy thông tin người dùng từ payload

                if (payload.Email == null)
                {
                    throw new Exception("Không có dữ liệu email từ Google API");
                }
                var email = payload.Email;
                var account = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Username.Equals(email) && x.Status.Equals(UserStatus.Active));
                if (account == null)
                {
                    throw new NotFoundException("Email không tồn tại trên hệ thống");
                }
                var accessToken = JWTHelper.GenerateToken(account.Username, account.Role, _configuration["JWTSettings:Key"]!, _configuration["JWTSettings:Issuer"]!, _configuration["JWTSettings:Audience"]!);
                var refreshToken = JWTHelper.GenerateRefreshToken();

                account.RefreshToken = refreshToken;
                account.AccessToken = accessToken;
                account.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);

                _unitOfWork.GetRepository<User>().UpdateAsync(account);
                bool isUpdate = await _unitOfWork.CommitAsync() > 0;
                if (!isUpdate)
                {
                    throw new Exception("Login failed");
                }

                var response = _mapper.Map<LoginResponse>(account);

                var expert = await _unitOfWork.GetRepository<Expert>().SingleOrDefaultAsync(predicate: x => x.UserId.Equals(account.Id));

                if (expert != null)
                {
                    response.ExpertInformation = _mapper.Map<ExpertInformation>(expert);
                }
                return response;
            }
            catch (InvalidJwtException ex)
            {
                throw new Exception("Id Token hoặc JWT không hợp lệ");
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
