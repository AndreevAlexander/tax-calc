using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Rules;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation
{
    public class DefaultRuleExecutionStrategy : IRuleExecutionStrategy
    {
        private readonly RuleResolver<IValidationRule> _ruleResolver;
        private readonly Dictionary<string, IEnumerable<ValidationResult>> _validationResults;

        public DefaultRuleExecutionStrategy(RuleResolver<IValidationRule> ruleResolver)
        {
            _ruleResolver = ruleResolver;
            _validationResults = new Dictionary<string, IEnumerable<ValidationResult>>();
        }

        public async Task<Dictionary<string, IEnumerable<ValidationResult>>> ExecuteValidationRulesAsync<TModel>(RuleConfiguration configuration, TModel model, object context = null) where TModel : class
        {
            _validationResults.Clear();

            var propertyName = configuration.PropertyName;
            var value = configuration.GetValue(model);

            if (configuration.IsRequired)
            {
                var validationResults = await new RequiredValidationRule().ValidateAsync(value, model);
                SetResults(propertyName, validationResults.ToList());
            }

            if (configuration.MaxLength != null)
            {
                var validationResults = await new MaxLengthValidationRule().ValidateAsync(value, model, configuration.MaxLength);
                SetResults(propertyName, validationResults.ToList());
            }

            if (configuration.MinLength != null)
            {
                var validationResults = await new MinLengthValidationRule().ValidateAsync(value, model, configuration.MinLength);
                SetResults(propertyName, validationResults.ToList());
            }

            if (configuration.Regex != null)
            {
                var validationResults = await new RegexValidationRule().ValidateAsync(value, model, configuration.Regex);
                SetResults(propertyName, validationResults.ToList());
            }

            if (configuration.IsNumeric)
            {
                var validationResults = await new NumericValidationRule().ValidateAsync(value, model);
                SetResults(propertyName, validationResults.ToList());
            }

            if (configuration.CustomValidators.Any())
            {
                foreach (var customValidatorType in configuration.CustomValidators)
                {
                    var customValidator = _ruleResolver(customValidatorType);
                    var validationResults = await customValidator.ValidateAsync(value, model, context);
                    SetResults(propertyName, validationResults.ToList());
                }
            }

            return _validationResults;
        }

        private void SetResults(string propertyName,
                                List<ValidationResult> validationResults)
        {
            if (validationResults.Any())
            {
                if (_validationResults.TryGetValue(propertyName, out IEnumerable<ValidationResult> existingResults))
                {
                    validationResults.AddRange(existingResults);
                    _validationResults[propertyName] = validationResults;
                }
                else
                {
                    _validationResults.Add(propertyName, validationResults);
                }
            }
        }
    }
}
