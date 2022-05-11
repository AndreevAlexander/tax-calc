using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Application.Taxes.Commands;
using TaxCalculator.Application.Taxes.Queries;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.WebApi.Extensions;

namespace TaxCalculator.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxController : BaseController
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

    [HttpGet("one")]
    public async Task<ActionResult<Tax>> GetTaxById([FromQuery] GetTaxByIdQuery query)
    {
        var taxes = await _queryBus.ExecuteAsync<GetTaxByIdQuery, Tax>(query);
        return Ok(taxes);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tax>>> GetTaxes([FromQuery] GetTaxesQuery query)
    {
        var taxes = await _queryBus.ExecuteAsync<GetTaxesQuery, List<Tax>>(query);
        return Ok(taxes);
    }

    [HttpPost]
    public async Task<ActionResult<ValidatedCommandResult>> CreateTax([FromBody] CreateTaxCommand command)
    {
        var validationResults = await _validationEngine.ValidateAsync(command);
        if (!validationResults.HasErrors)
        {
            var result = await _commandBus.DispatchAsync(command);
            return Ok(result.ToValidated(validationResults.ValidationResults)); 
        }

        return BadRequest(validationResults);
    }

    [HttpPut]
    public async Task<ActionResult<ValidatedCommandResult>> UpdateTax([FromBody] UpdateTaxCommand command)
    {
        var validationResults = await _validationEngine.ValidateAsync(command);
        if (!validationResults.HasErrors)
        {
            var result = await _commandBus.DispatchAsync(command);
            return Ok(result.ToValidated(validationResults.ValidationResults)); 
        }

        return BadRequest(validationResults);
    }
    
    [HttpDelete]
    public async Task<ActionResult<ValidatedCommandResult>> DeleteTax([FromQuery] RemoveTaxCommand command)
    {
        var validationResult = await _validationEngine.ValidateAsync(command);
        if (!validationResult.HasErrors)
        {
            var result = await _commandBus.DispatchAsync(command);
            return Ok(result.ToValidated(validationResult.ValidationResults)); 
        }

        return BadRequest(validationResult.ValidationResults);
    }
}