using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.Incomes.Commands;

public class UpdateIncomeHandler : ICommandHandler<UpdateIncomeCommand>
{
    private readonly WebApi _api;

    public UpdateIncomeHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(UpdateIncomeCommand command)
    {
        var result = await _api.Update(command, "Income");
        return result.RecordId.HasValue ? CommandResult.Success(result.RecordId) : CommandResult.Fail();
    }
}