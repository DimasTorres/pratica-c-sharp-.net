using FluentValidation.Results;
using Pratica.Domain.Models.Base;

namespace Pratica.Application.Validators.Base;

public static class GetValidationErrors
{
    public static Response GetErrors(this ValidationResult validationResult)
    {
        var response = new Response();
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                response.ReportErrors.Add(new ReportError
                {
                    Code = error.ErrorCode,
                    Message = error.ErrorMessage
                });
            }
        }

        return response;
    }
}
