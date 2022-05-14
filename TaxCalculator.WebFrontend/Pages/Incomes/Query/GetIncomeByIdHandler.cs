using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.Incomes.Query;

public class GetIncomeByIdHandler : IQueryHandler<GetIncomeByIdQuery, IncomeModel>
{
    private readonly WebApi _api;

    public GetIncomeByIdHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<IncomeModel> HandleAsync(GetIncomeByIdQuery query)
    {
        var income = await _api.GetOneAsync<IncomeModel>($"Income/one?IncomeId={query.IncomeId}");
        return income;
    }
}