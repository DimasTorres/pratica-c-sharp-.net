using FluentValidation;
using Pratica.Application.DataContract.User.Request;

namespace Pratica.Application.Validators;

public class AuthRequestValidator : AbstractValidator<AuthRequest>
{
    public AuthRequestValidator()
    {
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
    }
}
