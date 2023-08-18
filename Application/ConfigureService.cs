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
             services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
             services.AddAutoMapper(Assembly.GetExecutingAssembly());
             services.AddMediatR(option =>
            {
                 option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            });

             services.AddScoped<IUserRefreshToken, RefreshToken>();
             services.AddScoped<IJwtToken, JwtToken>();

            return services;
        }
    }
}
