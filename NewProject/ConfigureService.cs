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

        _ = services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        _ = services.AddEndpointsApiExplorer();
        _ = services.AddAuthorization();
        _ = services.AddScoped<IDocChangeLogger, LoggingService>();
        _ = services.AddScoped<ICurrentUserService, CurrentUserService>();
        //services.AddAutoMapper(typeof(UserMapping));
        //services.AddSingleton(provider => new MapperConfiguration(cfg =>
        //{
        //    cfg.AddProfile(new CategoryMapping(provider.GetService<ICurrentUserService>()));
        //    cfg.AddProfile(new UserMapping(provider.GetService<ICurrentUserService>()));
        //    cfg.AddProfile(new UserTypeMapping(provider.GetService<ICurrentUserService>()));
        //}).CreateMapper());

        _ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtSettings(configuration);
        _ = services.AddHttpContextAccessor();
        _ = services.AddSwaggerGen();
        _ = services.AddAutoMapper(typeof(CategoryMapping));

        _ = services.AddSwaggerGen(c =>
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
                    new string[] {}
                }
            });
            //  c.OperationFilter<SecurityRequirementsOperationFilter>();

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
