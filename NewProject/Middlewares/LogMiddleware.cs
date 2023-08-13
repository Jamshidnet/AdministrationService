using Application.Common.Abstraction;
using Application.Common.Extensions;
using Domein.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Middlewares;

public class ChangeLoggingMiddleware : IMiddleware
{
    public ChangeLoggingMiddleware(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        this.currentUserService = currentUserService;
    }

    public IApplicationDbContext _context { get; set; }
    public ICurrentUserService currentUserService { get; set; }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);
        var routeValues = context.Request.RouteValues;
        string tableName = routeValues["controller"] as string;
        string actionName = routeValues["action"] as string;

        await Log(actionName, tableName);
    }

    public async Task Log(string action, string tableName)
    {
        int TableId = ExtensionMethods.Tables
            .GetValueOrDefault($"{tableName}Table");

        var table = await _context.SysTables
            .SingleOrDefaultAsync(x => x.TableId == TableId)
            ?? throw new Exception(
                " there is no table with this table id . ");
        string UserName = currentUserService.Username;
        var user = _context.Users
            .SingleOrDefault(x => x.Username == UserName);


        UserAction Logitems = new()
        {
            ActionName = action,
            ActionTime = DateTime.Now,
            Id = Guid.NewGuid(),
            TableId = table.Id,
            UserId = user?.Id
        };


        await _context.UserActions.AddAsync(Logitems);
        await _context.SaveChangesAsync();
    }
}
