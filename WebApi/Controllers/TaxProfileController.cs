using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Application.TaxProfiles.Queries;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxProfileController : Controller
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public TaxProfileController(ICommandBus commandBus, IQueryBus queryBus)
    {
        _commandBus = commandBus;
        _queryBus = queryBus;
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
        var result = await _commandBus.DispatchAsync(command);
        return Ok(result);
    }
}