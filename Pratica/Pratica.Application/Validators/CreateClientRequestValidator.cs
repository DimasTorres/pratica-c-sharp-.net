using FluentValidation;
using Pratica.Application.DataContract.Client.Request;

namespace Pratica.Application.Validators;

public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
{
    public CreateClientRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .Length(3, 250);

        RuleFor(x => x.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .NotNull();
    }
}
