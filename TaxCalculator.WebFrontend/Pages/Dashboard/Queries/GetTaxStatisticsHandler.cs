using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.Dashboard.Queries;

public class GetTaxStatisticsHandler : IQueryHandler<GetTaxStatisticsQuery, CalculateTaxesModel?>
{
    private readonly WebApi _api;

    public GetTaxStatisticsHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CalculateTaxesModel?> HandleAsync(GetTaxStatisticsQuery query)
    {
        var result = await _api.GetOneAsync<CalculateTaxesModel?>($"TaxProfile/CalculateTaxes?ProfileId={query.ProfileId}");
        return result;
    }
}