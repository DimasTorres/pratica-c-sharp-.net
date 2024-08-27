using FluentValidation;
using Pratica.Application.DataContract.Product.Request;

namespace Pratica.Application.Validators;
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .Length(3, 150);
    }
}
