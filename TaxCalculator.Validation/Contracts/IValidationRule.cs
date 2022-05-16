using System.Collections.Generic;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Contracts;

public interface IValidationRule
{
    Task<IEnumerable<ValidationResult>> ValidateAsync(object? data, string propertyName, object? context = null);
}