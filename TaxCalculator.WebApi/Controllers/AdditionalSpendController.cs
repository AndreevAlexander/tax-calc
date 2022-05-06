using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Application.AdditionalSpends.Commands;
using TaxCalculator.Application.AdditionalSpends.Queries;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.WebApi.Extensions;

namespace TaxCalculator.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdditionalSpendController : BaseController
{
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;
    private readonly IValidationEngine _validationEngine;

    public AdditionalSpendController(IQueryBus queryBus, ICommandBus commandBus, IValidationEngine validationEngine)
    {
        _queryBus = queryBus;
        _commandBus = commandBus;
        _validationEngine = validationEngine;
    }

    [HttpGet("one")]
    public async Task<ActionResult<AdditionalSpend>> GetById([FromQuery] GetAdditionalSpendByIdQuery query)
    {
        var result = await _queryBus.ExecuteAsync<GetAdditionalSpendByIdQuery, AdditionalSpend?>(query);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<AdditionalSpend>>> GetSpends([FromQuery] GetAdditionalSpendsQuery query)
    {
        var results = await _queryBus.ExecuteAsync<GetAdditionalSpendsQuery, List<AdditionalSpend>>(query);

        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult<ValidatedCommandResult>> AddSpend([FromBody] AddAdditionalSpendCommand command)
    {
        var validationResults = _validationEngine.Validate(command);
        if (!validationResults.HasErrors)
        {
            var result = await _commandBus.DispatchAsync(command);
            return Ok(result.ToValidated(validationResults.ValidationResults));
        }

        return BadRequest(validationResults);
    }
    
    [HttpPut]
    public async Task<ActionResult<ValidatedCommandResult>> UpdateSpend([FromBody] UpdateAdditionalSpendCommand command)
    {
        var validationResults = _validationEngine.Validate(command);
        if (!validationResults.HasErrors)
        {
            var result = await _commandBus.DispatchAsync(command);
            return Ok(result.ToValidated(validationResults.ValidationResults));
        }

        return BadRequest(validationResults);
    }
    
    [HttpDelete]
    public async Task<ActionResult<ValidatedCommandResult>> DeleteAdditionalSpend([FromQuery] RemoveAdditionalSpendCommand command)
    {
        var validationResult = _validationEngine.Validate(command);
        if (!validationResult.HasErrors)
        {
            var result = await _commandBus.DispatchAsync(command);
            return Ok(result.ToValidated(validationResult.ValidationResults)); 
        }

        return BadRequest(validationResult.ValidationResults);
    }
}