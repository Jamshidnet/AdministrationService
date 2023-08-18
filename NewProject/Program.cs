using Application;
using Infrustructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewProject.Abstraction;
using NewProject.Middlewares;
using PublicAffairsPortal.WebUI.CustomAttributes;
using System;

namespace NewProject;

public class Program
{
    public static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var builder = WebApplication.CreateBuilder(args);
         builder.Services.AddApplication();
         builder.Services.AddApi(builder.Configuration);
         builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationExceptionFilter)));
         builder.Services.AddEndpointsApiExplorer();
         builder.Services.AddSwaggerGen();
         builder.Services.AddScoped<ChangeLoggingMiddleware>();
         builder.Services.AddTransient<IApplicationDbContext, NewdatabaseContext>();
         builder.Services.AddDbContext<NewdatabaseContext>(
            options =>
            {
                 options.UseNpgsql(builder.Configuration.GetConnectionString("dbconnect"));
                 options.UseLazyLoadingProxies();
            }
            ); ;
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
             app.UseSwagger();
             app.UseSwaggerUI(c =>
            {
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
             app.UseSwaggerUI();

        }

         app.UseHttpsRedirection();

         app.UseAuthentication();
         app.UseAuthorization();

         app.UseMiddleware<ChangeLoggingMiddleware>();
         app.MapControllers();

        app.Run();
    }
}


