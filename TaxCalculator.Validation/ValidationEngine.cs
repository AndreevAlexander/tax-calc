using System.Collections.Generic;
using System.Threading.Tasks;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation
{
    public class ValidationEngine : IValidationEngine
    {
        private readonly IRuleExecutionStrategy _executionStrategy;
        
        private readonly IProfileProvider _profileProvider;

        public ValidationEngine(IRuleExecutionStrategy executionStrategy,
            IProfileProvider profileProvider)
        {
            _executionStrategy = executionStrategy;
            _profileProvider = profileProvider;
        }

        public async Task<ValidationResultContainer> ValidateAsync<TModel>(TModel model, object context = null) where TModel : class
        {
            Dictionary<string, IEnumerable<ValidationResult>> validationResults = null;

            var profilesForModel = _profileProvider.GetRules<TModel>();

            foreach (var validationProfile in profilesForModel)
            {
                var ruleBuilder = validationProfile.GetRuleBuilder<TModel>();
                var ruleConfigurations = ruleBuilder.Build();

                foreach (var ruleConfiguration in ruleConfigurations)
                {
                    validationResults = await _executionStrategy.ExecuteValidationRulesAsync(ruleConfiguration, model, context);
                }
            }

            return new ValidationResultContainer(validationResults ?? new Dictionary<string, IEnumerable<ValidationResult>>());
        }
    }
}