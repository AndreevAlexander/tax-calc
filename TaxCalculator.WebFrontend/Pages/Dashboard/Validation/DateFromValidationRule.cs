using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.Dashboard.Validation;

public class DateFromValidationRule : IValidationRule
{
    public async Task<IEnumerable<ValidationResult>> ValidateAsync(object? data, object model, object? context = null)
    {
        var results = new List<ValidationResult>();

        if (model is TaxProfileDropdownModel dropdownModel && data is DateTime fromDate)
        {
            if (fromDate > dropdownModel.To)
            {
                results.Add(ValidationResult.Invalid("Date 'From' can not be greater than 'To'"));        
            }
        }

        return results;
    }
}