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
        var url = $"TaxProfile/CalculateTaxes?ProfileId={query.ProfileId}";
        if (query.CurrencyId.HasValue)
        {
            url = $"{url}&CurrencyId={query.CurrencyId}";
        }
        
        var result = await _api.GetOneAsync<CalculateTaxesModel?>(url);
        
        return result;
    }
}