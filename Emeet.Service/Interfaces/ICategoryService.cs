using Emeet.Service.DTOs.Responses.Authentication;
using Emeet.Service.DTOs.Responses.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<GetCategoryResponse>> GetAll();
    }
}
