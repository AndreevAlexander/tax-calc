using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Rules
{
    public class RegexValidationRule : IValidationRule
    {
        public async Task<IEnumerable<ValidationResult>> ValidateAsync(object data, object model, object context = null)
        {
            var result = new List<ValidationResult>();

            var pattern = (string)context;

            if (!string.IsNullOrEmpty(pattern) && !string.IsNullOrWhiteSpace(pattern))
            {
                if (data is string s && !Regex.IsMatch(s, pattern))
                {
                    result.Add(ValidationResult.Invalid($"Value does not match {pattern}"));
                }
            }
            else
            {
                throw new Exception("Incorrect regex pattern was set");
            }

            return result;
        }
    }
}