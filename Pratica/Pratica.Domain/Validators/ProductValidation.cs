using FluentValidation;
using Pratica.Domain.Models;

namespace Pratica.Domain.Validators
{
    public class ProductValidation : AbstractValidator<ProductModel>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .Length(3, 150);
        }
    }
}
