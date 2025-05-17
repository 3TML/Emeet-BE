using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Interfaces;
using Emeet.Domain.Specifications;
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

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}
