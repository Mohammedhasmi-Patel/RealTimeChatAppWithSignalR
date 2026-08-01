using ChatApp.Application.DTO.Auth.Requests;
using FluentValidation;

namespace ChatApp.Application.DTO.Auth.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.Avatar)
            .Must(x => x is null || x.Length <= 2 * 1024 * 1024)
            .WithMessage("Avatar must be 2 MB or less.");

    }
}
