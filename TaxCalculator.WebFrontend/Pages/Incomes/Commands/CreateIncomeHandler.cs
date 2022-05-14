using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.Incomes.Commands;

public class CreateIncomeHandler : ICommandHandler<CreateIncomeCommand>
{
    private readonly WebApi _api;

    public CreateIncomeHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(CreateIncomeCommand command)
    {
        var result = await _api.Create(command, "Income");
        return result.RecordId.HasValue ? CommandResult.Success(result.RecordId) : CommandResult.Fail();
    }
}