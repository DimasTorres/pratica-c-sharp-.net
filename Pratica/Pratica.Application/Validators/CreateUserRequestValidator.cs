using FluentValidation;
using Pratica.Application.DataContract.User.Request;

namespace Pratica.Application.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .Length(3, 150);

        RuleFor(x => x.Login)
            .NotEmpty()
            .NotNull()
            .Length(3, 20);

        RuleFor(m => m.Password)
         .NotEmpty().WithMessage("Password is required.")
         .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
         .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
         .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
         .Matches("[0-9]").WithMessage("Password must contain at least one number.")
         .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Confirm Password is not equal Password.");

        RuleFor(x => x.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .NotEmpty()
            .NotNull();
    }
}
