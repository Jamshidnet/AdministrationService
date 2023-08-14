using Application.Common.Extensions;
using Application.UseCases.Permissions.Responses;
using Application.UseCases.Roles.Responses;
using Domein.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewProject.JWT.Interfaces;
using NewProject.JWT.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Common.JWT.Service;

public class JwtToken : IJwtToken
{
    private readonly IConfiguration _configuration;
    private IUserRefreshToken refreshTokenService;
    public JwtToken(IConfiguration configuration, IUserRefreshToken refreshTokenService)
    {
        _configuration = configuration;
        this.refreshTokenService = refreshTokenService;
    }

    public TokenResponse CreateTokenAsync(string userName, string UserId, ICollection<RoleResponse> Roles, CancellationToken cancellationToken = default)
    {
        var claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, userName),
        new Claim(ClaimTypes.NameIdentifier,UserId)
    };

        List<PermissionResponse> permissions = new List<PermissionResponse>();

        foreach (var item in Roles)
        {
            foreach (var per in item.Permissions)
            {
                permissions.Add(per);
            }
        }

        if (Roles.Count > 0)
        {
            foreach (var role in Roles)
            {
                foreach (var permission in role.Permissions)
                {
                    claims.Add(new Claim(ClaimTypes.Role, permission.PermissionName));

                }

                // claims.Add(new Claim(ClaimTypes.Role, role.RoleName));

            }
        }

        var jwt = new JwtSecurityToken(
           issuer: _configuration.GetValue<string>("JWT:Issuer"),
           audience: _configuration.GetValue<string>("JWT:Audience"),
           claims: claims,
           expires: DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:TokenExpiredTimeAtDays", 10)),
           signingCredentials: new SigningCredentials(
                   new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key"))),
                           SecurityAlgorithms.HmacSha256));

        var responseModel = new TokenResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
            RefreshToken = GenerateRefreshTokenAsync(userName),
            Permissions = permissions
        };


        refreshTokenService.AddOrUpdateRefreshToken(new UserRefreshToken()
        {
            RefreshToken = responseModel.RefreshToken,
            ExpiresTime = DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:RefreshTokenExpiredTimeAtDays")),
            UserName = userName
        }, cancellationToken);

        return responseModel;
    }
    public string GenerateRefreshTokenAsync(string userName)
    {
        var refreshToken = (userName + DateTime.Now).GetHashedString();
        return refreshToken;
    }

    public ValueTask<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParametrs = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = false,
            ValidAudience = _configuration.GetValue<string>("JWT:Audience"),
            ValidIssuer = _configuration.GetValue<string>("JWT:Issuer"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")))

        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParametrs, out SecurityToken securityToken);
        JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return ValueTask.FromResult(principal);
    }
}
