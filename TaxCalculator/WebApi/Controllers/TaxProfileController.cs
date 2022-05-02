using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Application.TaxProfiles.Queries;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxProfileController : Controller
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IValidationEngine _validationEngine;

    public TaxProfileController(ICommandBus commandBus, IQueryBus queryBus, IValidationEngine validationEngine)
    {
        _commandBus = commandBus;
        _queryBus = queryBus;
        _validationEngine = validationEngine;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaxProfile>>> GetProfiles([FromQuery] GetTaxProfilesQuery query)
    {
        var result = await _queryBus.ExecuteAsync<GetTaxProfilesQuery, List<TaxProfile>>(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CommandResult>> CreateProfile([FromBody] CreateTaxProfileCommand command)
    {
        var validationResult = _validationEngine.Validate(command);
        if (!validationResult.HasErrors)
        {
            var result = await _commandBus.DispatchAsync(command);
            return Ok(result); 
        }

        return BadRequest(validationResult.ValidationResults);
    }

    [HttpGet("CalculateTaxes")]
    public async Task<ActionResult<CalculateTaxesResult>> CalculateTaxes([FromQuery] CalculateTaxesQuery query)
    {
        var result = await _queryBus.ExecuteAsync<CalculateTaxesQuery, CalculateTaxesResult>(query);
        return Ok(result);
    }
}