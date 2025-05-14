using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Service.DTOs.Requests.Authentication;
using Emeet.Service.DTOs.Responses.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Emeet.Service.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Admin 
            CreateMap<GetCategoryResponse, Category>().ReverseMap();
            CreateMap<RegisterRequest, User>().ReverseMap();
            CreateMap<RegisterRequest, Expert>().ReverseMap();
        }
    }
}
