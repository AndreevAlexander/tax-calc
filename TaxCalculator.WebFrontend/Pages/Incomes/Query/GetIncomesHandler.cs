using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.Incomes.Query;

public class GetIncomesHandler : IQueryHandler<GetIncomesQuery, List<IncomeModel>>
{
    private readonly WebApi _api;

    public GetIncomesHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<List<IncomeModel>> HandleAsync(GetIncomesQuery query)
    {
        var incomes = await _api.GetManyAsync<IncomeModel>($"Income?ProfileId={query.TaxProfileId}");
        return incomes.ToList();
    }
}