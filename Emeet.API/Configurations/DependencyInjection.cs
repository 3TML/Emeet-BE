using Emeet.Domain.Interfaces;
using Emeet.Infrastructure.Repository;
using Emeet.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication;

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
            //services.AddScoped<IAuthenticationService, AuthenticationService>();
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
