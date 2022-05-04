using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Validation;
using TaxCalculator.WebApi.Extensions;

namespace TaxCalculator.WebApi.Controllers;

public abstract class BaseController : Controller
{
    public override BadRequestObjectResult BadRequest(object? result)
    {
        if (result is ValidationResultContainer validationResultContainer)
        {
            return base.BadRequest(new ValidatedCommandResult
            {
                Status = CommandStatus.Fail,
                RecordId = null,
                ValidationResults = validationResultContainer.ValidationResults
            });
        }

        return base.BadRequest(result);
    }
}