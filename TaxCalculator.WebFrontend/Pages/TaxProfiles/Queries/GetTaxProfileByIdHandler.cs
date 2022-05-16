using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Queries;

public class GetTaxProfileByIdHandler : IQueryHandler<GetTaxProfileByIdQuery, TaxProfileModel?>
{
    private readonly WebApi _api;

    public GetTaxProfileByIdHandler(WebApi api)
    {
        _api = api;
    }
    public async Task<TaxProfileModel?> HandleAsync(GetTaxProfileByIdQuery query)
    {
        var profile = await _api.GetOneAsync<TaxProfileModel?>($"TaxProfile/one?TaxProfileId={query.TaxProfileId}");
        return profile;
    }
}