using System.Collections.Generic;
using System.Threading.Tasks;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Contracts
{
    public interface IValidationRuleBase
    {

    }

    public interface IValidationRule : IValidationRuleBase
    {
        Task<IEnumerable<ValidationResult>> ValidateAsync(object data, object model, object context = null);
    }
}