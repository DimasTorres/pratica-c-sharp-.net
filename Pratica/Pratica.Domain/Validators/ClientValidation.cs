using FluentValidation;
using Pratica.Domain.Models;

namespace Pratica.Domain.Validators
{
    public class ClientValidation : AbstractValidator<ClientModel>
    {
        public ClientValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .Length(3, 150);

            RuleFor(x => x.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull();
        }
    }
}
