using Application.Common.Abstraction;
using Application.Common.Logging;
using Application.Common.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NewProject.JWT;
using NewProject.Service;
using System.Text.Json.Serialization;

namespace NewProject;

public static class ConfigureService
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {

         services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
         services.AddEndpointsApiExplorer();
         services.AddAuthorization();
         services.AddScoped<IDocChangeLogger, LoggingService>();
         services.AddScoped<ICurrentUserService, CurrentUserService>();
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtSettings(configuration);
         services.AddHttpContextAccessor();
         services.AddSwaggerGen();
         services.AddAutoMapper(typeof(CategoryMapping));
         services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Public Affairs Portal API" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Description = "Please insert JWT token into field"

            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"

                        }
                    },
                    System.Array.Empty<string>()
                }
            });
        });

        return services;
    }
}
