using FluentValidation;
using Pratica.Application.DataContract.Order.Request;

namespace Pratica.Application.Validators;

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(x => x.UserId)
              .NotNull();

        RuleFor(x => x.ClientId)
            .NotNull();
    }
}
