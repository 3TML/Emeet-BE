using Emeet.Service.DTOs.Responses.Expert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Interfaces
{
    public interface IExpertService
    {
        Task<List<GetSuggestionExpert>> GetSuggestionExperts();
    }
}
