using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Enums;
using Emeet.Domain.Interfaces;
using Emeet.Domain.Specifications;
using Emeet.Service.DTOs.Responses.Feedback;
using Emeet.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPaginate<GetFeedbackExpert>> GetFeedbackExpert(Guid ExpertId, int page, int size)
        {
            var feedbacks = await _unitOfWork.GetRepository<Feedback>().GetPagingListAsync(predicate: x => x.ExpertId == ExpertId && x.Status.Equals(FeedbackStatus.Active), page: page, size: size);
            return new Paginate<GetFeedbackExpert>()
            {
                Page = feedbacks.Page,
                Size = feedbacks.Size,
                TotalPages = feedbacks.TotalPages,
                Items = _mapper.Map<IList<GetFeedbackExpert>>(feedbacks.Items)
            };
        }
    }
}
