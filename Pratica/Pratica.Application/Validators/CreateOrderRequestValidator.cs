using FluentValidation;
using Pratica.Application.DataContract.Order.Request;

namespace Pratica.Application.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId)
               .NotNull();

        RuleFor(x => x.ClientId)
            .NotNull();
    }
}
