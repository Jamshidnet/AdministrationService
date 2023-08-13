using Application.Common.Abstraction;
using Application.Common.Logging;
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


        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Example API", Version = "v1" });

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
                    new string[] { }
                }
            });

        });

        //Log.Logger = new LoggerConfiguration()
        //   .WriteTo.PostgreSQL(
        //       connectionString:configuration.GetConnectionString("dbconnect"),
        //       tableName: "UserLogs",
        //       schemaName: "public",
        //       needAutoCreateTable: true,
        //columnOptions: new ColumnOptions
        //{
        //           AdditionalColumns = new Collection<SqlColumn>
        //           {
        //        new SqlColumn { ColumnName = "AffectedTableName", DataType = NpgsqlTypes.NpgsqlDbType.Varchar },
        //        new SqlColumn { ColumnName = "ActionTime", DataType = NpgsqlTypes.NpgsqlDbType.Timestamp },
        //        new SqlColumn { ColumnName = "ActionName", DataType = NpgsqlTypes.NpgsqlDbType.Varchar },
        //        new SqlColumn { ColumnName = "UserId", DataType = NpgsqlTypes.NpgsqlDbType.Varchar }
        //    }
        //       })
        //   .CreateLogger();

        //services.AddLogging(loggingBuilder =>
        //{
        //    loggingBuilder.AddSerilog(dispose: true);
        //});


        return services;
    }
}
