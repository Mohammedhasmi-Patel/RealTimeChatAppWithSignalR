using ChatApp.Application.DTO.Auth.Requests;
using FluentValidation;

namespace ChatApp.Application.DTO.Auth.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(50)
                .WithMessage("First name cannot exceed 50 characters.");


        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(50)
            .WithMessage("Last name cannot exceed 50 characters.");


        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Please enter a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.Avatar)
            .Must(x => x is null || x.Length <= 2 * 1024 * 1024)
            .WithMessage("Avatar must be 2 MB or less.");

    }
}
