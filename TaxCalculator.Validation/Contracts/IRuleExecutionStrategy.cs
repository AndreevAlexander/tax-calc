using System.Collections.Generic;
using System.Threading.Tasks;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Contracts
{
    public interface IRuleExecutionStrategy
    {
        Task<Dictionary<string, IEnumerable<ValidationResult>>> ExecuteValidationRulesAsync<TModel>(
            RuleConfiguration configuration,
            TModel model,
            object context = null) where TModel : class;
    }
}
