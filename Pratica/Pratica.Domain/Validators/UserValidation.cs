using FluentValidation;
using Pratica.Domain.Models;

namespace Pratica.Domain.Validators
{
    public class UserValidation : AbstractValidator<UserModel>
    {
        public UserValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .Length(3, 150);

            RuleFor(x => x.Login)
                .NotEmpty()
                .NotNull()
                .Length(3, 20);

            RuleFor(x => x.PasswordHash)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .NotEmpty()
                .NotNull();
        }
    }
}
