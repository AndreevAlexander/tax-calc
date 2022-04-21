﻿using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Application.Taxes.Commands;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxController : Controller
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IValidationEngine _validationEngine;

    public TaxController(ICommandBus commandBus, IQueryBus queryBus, IValidationEngine validationEngine)
    {
        _commandBus = commandBus;
        _queryBus = queryBus;
        _validationEngine = validationEngine;
    }

    [HttpPost]
    public async Task<ActionResult<CommandResult>> CreateTax([FromBody] CreateTaxCommand command)
    {
        var validationResults = _validationEngine.Validate(command);
        if (!validationResults.HasErrors)
        {
            var result = await _commandBus.DispatchAsync(command);
            return Ok(result);   
        }

        return BadRequest(validationResults.ValidationResults);
    }
}