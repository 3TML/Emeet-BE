using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Enums;
using Emeet.Domain.Interfaces;
using Emeet.Domain.Specifications;
using Emeet.Service.DTOs.Responses.Expert;
using Emeet.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Services
{
    public class ExpertService : IExpertService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpertService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetExpertByIdResponse> GetExpertById(Guid expertId)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync( 
                                                                  predicate: x=>x.Experts.SingleOrDefault()!.Id == expertId,
                                                                  include: x=>x.Include(s=>s.Experts)
                                                              );
            var result = _mapper.Map<GetExpertByIdResponse>(user);
            result = _mapper.Map<GetExpertByIdResponse>(user.Experts.SingleOrDefault());

            return result;
        }

        public async Task<IPaginate<GetExpertResponse>> GetExpertByNameCategory(string name, string category, int page, int size)
        {
            var user = await _unitOfWork.GetRepository<User>().GetPagingListAsync(
                                                                    predicate: x=>x.FullName.Contains(name) 
                                                                                && x.Experts.Any(s=>s.ExpertCategories.Any(s=>s.Category.Name.Contains(category))),
                                                                    page: page,
                                                                    size: size,
                                                                    include: q => q.Include(u => u.Experts)
                                                                                    .ThenInclude(e => e.ExpertCategories)
                                                                                        .ThenInclude(ec => ec.Category)
                                                                 );
            return new Paginate<GetExpertResponse>()
            {
                Page = user.Page,
                Size = user.Size,
                TotalPages = user.TotalPages,
                Items = _mapper.Map<IList<GetExpertResponse>>(user.Items)
            };
        }

        public async Task<List<GetSuggestionExpert>> GetSuggestionExperts()
        {
            var userExpert = await _unitOfWork.GetRepository<User>()
                   .GetListAsync(
                       predicate: x => x.Status.Equals(UserStatus.Active) && x.Role.Equals(RoleUser.Expert),
                       orderBy: q => q.OrderByDescending(s => s.Experts.SingleOrDefault()!.Rate)
                                     .ThenByDescending(s => s.Experts.SingleOrDefault()!.TotalReview),
                       take: 20,
                       include: x=>x.Include(s=>s.Experts)
                   );

            var expertSuggestionResponses = new List<GetSuggestionExpert>();
            foreach(var user in userExpert)
            {
                var expert = user.Experts.SingleOrDefault();
                var expertSuggestion = new GetSuggestionExpert() 
                {
                    Avatar = user.Avatar,
                    Bio = user.Bio,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    Experience = expert!.Experience,
                    Rate = expert.Rate,
                    TotalPreview = expert.TotalReview,
                };
                var listCategory = await _unitOfWork.GetRepository<Category>()
                    .GetListAsync(predicate: x => x.ExpertCategories.SingleOrDefault()!.ExpertId == expert.Id);
                expertSuggestion.ListCategory = listCategory.Select(s => s.Name).ToList();
                expertSuggestionResponses.Add(expertSuggestion);
            }
            return expertSuggestionResponses;
        }
    }
}
