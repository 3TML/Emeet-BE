using Emeet.Domain.Specifications;
using Emeet.Service.DTOs.Responses.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Interfaces
{
    public interface IFeedbackService
    {
        Task<IPaginate<GetFeedbackExpert>> GetFeedbackExpert(Guid ExpertId, int page, int size);
    }
}
