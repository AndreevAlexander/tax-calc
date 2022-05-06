using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Application.Incomes.Commands;
using TaxCalculator.Application.Incomes.Queries;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.WebApi.Extensions;

namespace TaxCalculator.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomeController : Controller
{
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;
    private readonly IValidationEngine _validationEngine;

    public IncomeController(IQueryBus queryBus, ICommandBus commandBus, IValidationEngine validationEngine)
    {
        _queryBus = queryBus;
        _commandBus = commandBus;
        _validationEngine = validationEngine;
    }

    [HttpGet("one")]
    public async Task<ActionResult<Income>> GetOne([FromQuery] GetIncomeByIdQuery query)
    {
        var income = await _queryBus.ExecuteAsync<GetIncomeByIdQuery, Income>(query);

        return Ok(income);
    }

    [HttpGet]
    public async Task<ActionResult<List<Income>>> GetIncomes([FromQuery] GetIncomesQuery query)
    {
        var incomes = await _queryBus.ExecuteAsync<GetIncomesQuery, List<Income>>(query);

        return Ok(incomes);
    }

    [HttpPost]
    public async Task<ActionResult<ValidatedCommandResult>> CreateIncome([FromBody] AddIncomeCommand command)
    {
        var validationResult = _validationEngine.Validate(command);
        if (!validationResult.HasErrors)
        {
            var result = await _commandBus.DispatchAsync(command);
            return Ok(result.ToValidated(validationResult.ValidationResults));
        }

        return BadRequest(validationResult.ValidationResults);
    }

    [HttpPut]
    public async Task<ActionResult<ValidatedCommandResult>> UpdateIncome([FromBody] UpdateIncomeCommand command)
    {
        var validationResult = _validationEngine.Validate(command);
        if (!validationResult.HasErrors)
        {
            var result = await _commandBus.DispatchAsync(command);
            return Ok(result.ToValidated(validationResult.ValidationResults));
        }

        return BadRequest(validationResult.ValidationResults);
    }
    
    [HttpDelete]
    public async Task<ActionResult<ValidatedCommandResult>> DeleteIncome([FromQuery] RemoveIncomeCommand command)
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