using FluentValidation;

namespace Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {

            _ = RuleFor(x => x.Username).NotEmpty().NotNull();
            _ = RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
