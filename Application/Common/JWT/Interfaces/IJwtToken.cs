using Application.UseCases.Roles.Responses;
using Domein.Entities;
using NewProject.JWT.Models;
using System.Security.Claims;

namespace NewProject.JWT.Interfaces
{
    public interface IJwtToken
    {
        TokenResponse CreateTokenAsync(string userName, string UserId, ICollection<RoleResponse> roles, CancellationToken cancellationToken = default);
        ValueTask<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshTokenAsync(string userName);
    }
}
