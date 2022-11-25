using System.Collections.Generic;
using System.Threading.Tasks;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Rules
{
    public class NumericValidationRule : IValidationRule
    {
        public async Task<IEnumerable<ValidationResult>> ValidateAsync(object data, object model, object context = null)
        {
            var results = new List<ValidationResult>();

            if (data is string s)
            {
                var isNumeric = decimal.TryParse(s, out decimal result);
                if (!isNumeric)
                {
                    results.Add(ValidationResult.Invalid("Value should be numeric"));
                }
            }
            else if (!(data is int) && !(data is double) && !(data is float) && !(data is decimal))
            {
                results.Add(ValidationResult.Invalid("Value should be numeric"));
            }

            return results;
        }
    }
}