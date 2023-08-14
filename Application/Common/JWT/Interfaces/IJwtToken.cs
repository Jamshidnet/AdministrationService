using Application.UseCases.Users.Responses;
using NewProject.JWT.Models;
using System.Security.Claims;

namespace NewProject.JWT.Interfaces
{
    public interface IJwtToken
    {
        TokenResponse CreateTokenAsync(UserResponse user, CancellationToken cancellationToken = default);
        ValueTask<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshTokenAsync(string userName);
    }
}
