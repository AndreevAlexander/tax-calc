using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.Dashboard.Validation;

public class DateToValidationRule : IValidationRule
{
    public async Task<IEnumerable<ValidationResult>> ValidateAsync(object? data, object model, object? context = null)
    {
        var results = new List<ValidationResult>();
        
        if (model is TaxProfileDropdownModel dropdownModel && data is DateTime toDate)
        {
            if (toDate < dropdownModel.From)
            {
                results.Add(ValidationResult.Invalid("Date 'To' can not be less than 'From'"));        
            }
        }

        return results;
    }
}