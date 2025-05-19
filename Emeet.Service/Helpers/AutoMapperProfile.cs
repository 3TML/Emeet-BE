using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Service.DTOs.Requests.Authentication;
using Emeet.Service.DTOs.Requests.Schedule;
using Emeet.Service.DTOs.Responses.Authentication;
using Emeet.Service.DTOs.Responses.Category;
using Emeet.Service.DTOs.Responses.Expert;
using Emeet.Service.DTOs.Responses.Schedule;
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
            CreateMap<Expert, GetSuggestionExpert>().ReverseMap();
            CreateMap<User, GetExpertByIdResponse>().ReverseMap();
            CreateMap<Expert, GetExpertByIdResponse>().ReverseMap();
            CreateMap<User, GetExpertResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experts.FirstOrDefault().Experience))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Experts.FirstOrDefault().Rate))
                .ForMember(dest => dest.TotalPreview, opt => opt.MapFrom(src => src.Experts.FirstOrDefault().TotalReview))
                .ForMember(dest => dest.ListCategory, opt => opt.MapFrom(src =>
                    src.Experts.FirstOrDefault().ExpertCategories.Select(c => c.Category.Name).ToList()));
            CreateMap<GetCertificatesResponse, Expert>().ReverseMap();

            //Category
            CreateMap<GetCategoryResponse, Category>().ReverseMap();

            //Schedule
            CreateMap<Schedule, ScheduleInformation>().ReverseMap();
            CreateMap<Schedule, GetScheduleResponse>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.HasValue ? src.StartTime.Value.ToString(@"hh\:mm") : null))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.HasValue ? src.EndTime.Value.ToString(@"hh\:mm") : null));
        }
    }
}
