using AutoMapper;
using AutoMapper.Configuration;
using Emeet.Domain.Entities;
using Emeet.Domain.Interfaces;
using Emeet.Service.DTOs.Responses.Authentication;
using Emeet.Service.DTOs.Responses.Category;
using Emeet.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuaration;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, Microsoft.Extensions.Configuration.IConfiguration configuaration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuaration = configuaration;
            _mapper = mapper;
        }
        public async Task<List<GetCategoryResponse>> GetAll()
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetListAsync();
            return _mapper.Map<List<GetCategoryResponse>>(categories);
        }
    }
}
