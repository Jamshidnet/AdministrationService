using Application.Common.JWT.Service;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewProject.JWT.Interfaces;
using System.Reflection;

namespace Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
            _ = services.AddMediatR(option =>
            {
                _ = option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            });

            _ = services.AddScoped<IUserRefreshToken, RefreshToken>();
            _ = services.AddScoped<IJwtToken, JwtToken>();

            return services;
        }
    }
}
