using ChatApp.Application.DTO.Auth.Requests;
using FluentValidation;

namespace ChatApp.Application.DTO.Auth.Validators;

public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Email is required")
                    .EmailAddress()
                    .WithMessage("Email is in proper format.");
        RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Password is required.")
                    .MinimumLength(8)
                    .WithMessage("Password must be at least 6 characters.");
    }
}
