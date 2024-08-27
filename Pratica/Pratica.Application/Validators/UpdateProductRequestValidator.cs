using FluentValidation;
using Pratica.Application.DataContract.Product.Request;

namespace Pratica.Application.Validators;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .Length(3, 150);
    }
}
