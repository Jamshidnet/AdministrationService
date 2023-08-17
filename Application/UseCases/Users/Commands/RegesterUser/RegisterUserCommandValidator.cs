using FluentValidation;

namespace Application.UseCases.Users.Commands.RegesterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        _ = RuleFor(user => user.FirstName).NotEmpty().WithMessage("FirstName name is required.");
        _ = RuleFor(user => user.Username).NotEmpty().WithMessage("Username is required.");
        _ = RuleFor(user => user.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+998(33|9[0-9])\d{7}$")
                .WithMessage("Phone must be in the format of '+998 90 123 45 67'.");

        _ = RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                   .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                   .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                   .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                   .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                   .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        _ = RuleFor(user => user.ConfirmPassword)
            .Equal(user => user.Password).WithMessage("Passwords do not match.");
    }
}