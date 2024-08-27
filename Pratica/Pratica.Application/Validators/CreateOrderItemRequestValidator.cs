using FluentValidation;
using Pratica.Application.DataContract.Order.Request;

namespace Pratica.Application.Validators;

public class CreateOrderItemRequestValidator : AbstractValidator<CreateOrderItemRequest>
{
    public CreateOrderItemRequestValidator()
    {

    }
}
