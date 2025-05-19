using Emeet.Service.DTOs.Requests.ExpertService;
using Emeet.Service.DTOs.Responses.ExpertService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Interfaces
{
    public interface IExServiceService
    {
        Task<IList<ExServiceResponse>> GetServiceByExpertId(Guid expertId);

        Task<bool> CreateService(CreateServiceRequest request);
        Task<bool> UpdateService(Guid id, UpdateServiceRequest request);
        Task<bool> DeleteServiceById(Guid id);
    }
}
