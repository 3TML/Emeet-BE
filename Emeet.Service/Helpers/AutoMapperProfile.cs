using AutoMapper;
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
            //CreateMap<AdminLoginResponse, Admin>().ReverseMap();
        }
    }
}
