using Emeet.Domain.Specifications;
using Emeet.Service.DTOs.Requests.Expert;
using Emeet.Service.DTOs.Requests.User;
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
        Task<GetExpertByIdResponse> GetExpertById(Guid expertId);
        Task<IPaginate<GetExpertResponse>> GetExpertByNameCategory(string name, string category, int page, int size);
        Task<bool> UploadCertificate(UploadCertificateRequest request);
        Task<bool> DeleteCertificateById(DeleteCertificateRequest request);
        Task<IPaginate<GetCertificatesResponse>> GetCertificatesByExpertId(Guid ExpertId, int page, int size);
    }
}
