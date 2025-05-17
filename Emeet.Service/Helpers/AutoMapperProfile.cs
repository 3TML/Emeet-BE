using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Service.DTOs.Requests.Authentication;
using Emeet.Service.DTOs.Responses.Authentication;
using Emeet.Service.DTOs.Responses.Category;
using Emeet.Service.DTOs.Responses.User;
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
            //User
            CreateMap<RegisterRequest, User>().ReverseMap();
            CreateMap<GetUserResponse, User>().ReverseMap();

            //Authentication
            CreateMap<RegisterRequest, Expert>().ReverseMap();
            CreateMap<User, LoginResponse>().ForMember(dest => dest.ExpertInformation, opt => opt.Ignore());
            //Expert
            CreateMap<Expert, ExpertInformation>().ReverseMap();

            //Category
            CreateMap<GetCategoryResponse, Category>().ReverseMap();
        }
    }
}
