using Application;
using Infrustructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewProject.Abstraction;
using System;

namespace NewProject;

public class Program
{
    public static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddApplication();
        // Add services to the container.
        builder.Services.AddApi(builder.Configuration);
      //  builder.Services.AddScoped<CustomAuthorizeAttribute>();
        builder.Services.AddControllers();
       // builder.Services.AddAuthorization();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IApplicationDbContext, NewdatabaseContext>();
        builder.Services.AddDbContext<NewdatabaseContext>(
            options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("dbconnect"));
                options.UseLazyLoadingProxies();
            }
            ); ;
        var app = builder.Build();

        // Configure the HTTP request pipeline.
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


        app.MapControllers();

        app.Run();
    }
}