using MediatR;
using NewProject.JWT.Interfaces;
using NewProject.JWT.Models;

namespace Application.UseCases.Users.Commands.LoginUser;

public class LoginUserCommand : IRequest<TokenResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponse>
{
    private readonly IJwtToken _jwtToken;
    private readonly IUserRefreshToken _userRefreshToken;

    public LoginUserCommandHandler(IJwtToken jwtToken, IUserRefreshToken userRefreshToken)
    {
        _jwtToken = jwtToken;
        _userRefreshToken = userRefreshToken;

    }

    public async Task<TokenResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {



        var authenUser = await _userRefreshToken.AuthenAsync(request);

        var tokenResponse = _jwtToken.CreateTokenAsync(authenUser.Username, authenUser.Id.ToString(), authenUser.Roles, cancellationToken);

        return tokenResponse;
    }
}
