using AutoMapper;
using Emeet.Domain.Interfaces;
using Emeet.Domain.Specifications;
using Emeet.Infrastructure.Repository;
using Emeet.Infrastructure.UnitOfWork;
using Emeet.Service.Helpers;
using Emeet.Service.Interfaces;
using Emeet.Service.Services;

namespace Emeet.API.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExpertService, ExpertService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IScheduleService, ScheduleService>();

            services.AddScoped(typeof(IPaginate<>), typeof(Paginate<>));

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        public static IServiceCollection AddDIRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }

        public static IServiceCollection AddDIAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

    }
}
