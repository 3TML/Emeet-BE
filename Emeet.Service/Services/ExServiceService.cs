using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Enums;
using Emeet.Domain.Exceptions;
using Emeet.Domain.Interfaces;
using Emeet.Service.DTOs.Requests.ExpertService;
using Emeet.Service.DTOs.Responses.ExpertService;
using Emeet.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Services
{
    public class ExServiceService : IExServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateService(CreateServiceRequest request)
        {
            var expert = await _unitOfWork.GetRepository<Expert>().SingleOrDefaultAsync(predicate: x => x.Id == request.ExpertId);
            if (expert == null)
            {
                throw new NotFoundException($"Expert not found with id: {request.ExpertId}");
            }
            var service = _mapper.Map<ExService>(request);
            service.CreatedAt = DateTime.Now;
            service.Status = ServiceStatus.Active;
            await _unitOfWork.GetRepository<ExService>().InsertAsync(service);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteServiceById(Guid id)
        {
            var service = await _unitOfWork.GetRepository<ExService>().SingleOrDefaultAsync(predicate: x => x.Id == id);
            service.Status = ServiceStatus.Inactive;
            service.UpdatedAt = DateTime.Now;
            _unitOfWork.GetRepository<ExService>().UpdateAsync(service);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IList<ExServiceResponse>> GetServiceByExpertId(Guid expertId)
        {
            var services = await _unitOfWork.GetRepository<ExService>().GetListAsync(predicate: x => x.ExpertId == expertId && x.Status.Equals(ServiceStatus.Active));
            var serviceList = _mapper.Map<IList<ExServiceResponse>>(services);
            return serviceList;
        }

        public async Task<bool> UpdateService(Guid id, UpdateServiceRequest request)
        {
            var service = await _unitOfWork.GetRepository<ExService>().SingleOrDefaultAsync(predicate: x => x.Id == id);
            if (service == null)
            {
                throw new NotFoundException($"Service not found with id: {id}");
            }
            service.Name = request.Name;
            service.Description = request.Description;
            service.Price = request.Price;
            service.Time = request.Time;
            _unitOfWork.GetRepository<ExService>().UpdateAsync(service);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
