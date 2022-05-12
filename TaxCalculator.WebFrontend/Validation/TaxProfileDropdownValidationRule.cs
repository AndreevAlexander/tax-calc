using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;
using TaxCalculator.WebFrontend.Models;
using TaxCalculator.WebFrontend.Pages.Taxes.Queries;

namespace TaxCalculator.WebFrontend.Validation;

public class TaxProfileDropdownValidationRule : IValidationRule
{
    private readonly IQueryBus _queryBus;

    public TaxProfileDropdownValidationRule(IQueryBus queryBus)
    {
        _queryBus = queryBus;
    }
    
    public async Task<IEnumerable<ValidationResult>> ValidateAsync(object? data, string propertyName, object? context = null)
    {
        var results = new List<ValidationResult>();
        
        var profileId = (string) data;

        var taxes = await _queryBus.ExecuteAsync<GetTaxesQuery, List<TaxModel>>(new GetTaxesQuery
            {ProfileId = Guid.Parse(profileId)});
        
        if (!taxes.Any())
        {
            results.Add(ValidationResult.Invalid("This profile misses tax configuration"));
        }

        return results;
    }
}