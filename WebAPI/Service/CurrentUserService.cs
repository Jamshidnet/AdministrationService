using Application.Common.Abstraction;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NewProject.Service;


public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

    }

    public string Username => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);


    public string LanguageId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.UserData);


}