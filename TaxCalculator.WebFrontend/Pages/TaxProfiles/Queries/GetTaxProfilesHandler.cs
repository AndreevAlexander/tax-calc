using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Queries;

public class GetTaxProfilesHandler : IQueryHandler<GetTaxProfilesQuery, List<TaxProfileModel>>
{
    private readonly WebApi _api;

    public GetTaxProfilesHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<List<TaxProfileModel>> HandleAsync(GetTaxProfilesQuery query)
    {
        IEnumerable<TaxProfileModel> profiles;

        if (query.Page.HasValue && query.PageSize.HasValue)
        {
            profiles = await _api.GetManyAsync<TaxProfileModel>(
                $"TaxProfile?Page{query.Page}&PageSize={query.PageSize}");
        }
        else
        {
            profiles = await _api.GetManyAsync<TaxProfileModel>("TaxProfile");    
        }
        
        return profiles.ToList();
    }
}