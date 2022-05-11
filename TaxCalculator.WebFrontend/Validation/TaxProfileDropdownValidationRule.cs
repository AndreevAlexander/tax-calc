using TaxCalculator.Domain.Entities;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Validation;

public class TaxProfileDropdownValidationRule : IValidationRule
{
    private readonly WebApi _webApi;

    public TaxProfileDropdownValidationRule(WebApi webApi)
    {
        _webApi = webApi;
    }
    
    public async Task<IEnumerable<ValidationResult>> ValidateAsync(object? data, string propertyName, object? context = null)
    {
        var results = new List<ValidationResult>();
        
        var profileId = (string) data;

        var taxes = await _webApi.GetManyAsync<Tax>($"Tax?ProfileId={profileId}");
        if (!taxes.Any())
        {
            results.Add(ValidationResult.Invalid("This profile misses tax configuration"));
        }

        return results;
    }
}