using Application.UseCases.Users.Commands.LoginUser;
using Application.UseCases.Users.Responses;
using Domein.Entities.Identity;

namespace NewProject.JWT.Interfaces
{
    public interface IUserRefreshToken
    {
        ValueTask<UserRefreshToken> AddOrUpdateRefreshToken(UserRefreshToken refreshToken, CancellationToken cancellationToken = default);
        ValueTask<UserResponse> AuthenAsync(LoginUserCommand user);
        ValueTask<bool> DeleteUserRefreshTokens(string username, string refreshToken, CancellationToken cancellationToken = default);
        ValueTask<UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken);
    }
}
