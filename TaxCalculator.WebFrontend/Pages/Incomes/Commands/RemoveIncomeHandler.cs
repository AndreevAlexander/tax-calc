using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.Incomes.Commands;

public class RemoveIncomeHandler : ICommandHandler<RemoveIncomeCommand>
{
    private readonly WebApi _api;

    public RemoveIncomeHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(RemoveIncomeCommand command)
    {
        var result = await _api.Remove($"Income?IncomeId={command.IncomeId}");
        return result.RecordId.HasValue ? CommandResult.Success() : CommandResult.Fail();
    }
}