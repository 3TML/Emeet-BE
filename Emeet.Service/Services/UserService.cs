using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Enums;
using Emeet.Domain.Exceptions;
using Emeet.Domain.Interfaces;
using Emeet.Domain.Specifications;
using Emeet.Service.DTOs.Requests.User;
using Emeet.Service.DTOs.Responses.User;
using Emeet.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediaService = mediaService;
        }

        public async Task<IPaginate<GetUserResponse>> GetAllUser(string fullName, string role, int page, int size)
        {
            var users = await _unitOfWork.GetRepository<User>().GetPagingListAsync(
                predicate: x=>x.FullName.Contains(fullName) && x.Role.Contains(role),
                page: page,
                size: size
            );

            var mappedItems = _mapper.Map<IList<GetUserResponse>>(users.Items);

            return new Paginate<GetUserResponse>
            {
                Page = users.Page,
                Size = users.Size,
                Total = users.Total,
                TotalPages = users.TotalPages,
                Items = mappedItems
            };
        }

        public async Task<bool> UpdateProfileById(Guid id, UpdateUserRequest updateUserRequest)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x=>x.Id == id);
            if (user == null)
            {
                throw new NotFoundException($"User not found with id: {id}");
            }

            user.FullName = updateUserRequest.FullName ?? user.FullName;
            user.Bio = updateUserRequest.Bio ?? user.Bio;
            user.Gender = updateUserRequest.Gender ?? user.Gender;
            user.DateUpdated = DateTime.UtcNow;
            if (updateUserRequest.Avatar != null)
            {
                var urlImg = await _mediaService.UploadAnImage(updateUserRequest.Avatar, MediaPath.AVATAR, user.Id.ToString());
                user.Avatar = urlImg;
            }
            _unitOfWork.GetRepository<User>().UpdateAsync(user);

            if (user.Role.Equals(RoleUser.Expert) && updateUserRequest.UpdateExpertRequest!=null)
            {
                var expert = await _unitOfWork.GetRepository<Expert>().SingleOrDefaultAsync(predicate: x => x.UserId == id);
                expert.Experience = updateUserRequest.UpdateExpertRequest.Experience ?? expert.Experience;
                _unitOfWork.GetRepository<Expert>().UpdateAsync(expert);
            }

            bool isCommit = await _unitOfWork.CommitAsync() > 0;
            if (!isCommit)
            {
                throw new Exception("Update user failed");
            }
            return isCommit;
        }
    }
}
