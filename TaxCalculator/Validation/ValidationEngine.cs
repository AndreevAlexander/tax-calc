using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;
using TaxCalculator.Validation.Rules;

namespace TaxCalculator.Validation;

public class ValidationEngine : IValidationEngine
{
	private readonly RuleResolver _ruleResolver;
	private readonly List<IValidationProfile> _validationProfiles;

	public ValidationEngine(RuleResolver ruleResolver)
	{
		_ruleResolver = ruleResolver;
		_validationProfiles = new List<IValidationProfile>();
	}

	public ValidationResultContainer Validate<TModel>(TModel model, object? context = null)
	{
		var results = new Dictionary<string, IEnumerable<ValidationResult>>();

		var profilesForModel = _validationProfiles.Where(x => x.HasRules<TModel>()).ToArray();
		
		foreach (var validationProfile in profilesForModel)
		{
			var ruleBuilder = validationProfile.GetRuleBuilder<TModel>();
			var ruleConfigurations = ruleBuilder.Build();

			foreach (var ruleConfiguration in ruleConfigurations)
			{
				var propertyNameForValidation = ruleConfiguration.Key;
				var propertyValidationConfiguration = ruleConfiguration.Value;

				var property = typeof(TModel).GetProperty(propertyNameForValidation);

				var value = property?.GetValue(model);

				var resultsForProperty = ExecuteValidationRules(propertyValidationConfiguration,
					propertyNameForValidation, value, context);
				
				foreach (var result in resultsForProperty)
				{
					results.Add(result.Key, result.Value);
				}
			}
		}
		
		return new ValidationResultContainer(results);
	}

	private Dictionary<string, IEnumerable<ValidationResult>> ExecuteValidationRules(RuleConfiguration configuration,
		string propertyName, object? value, object? context = null)
	{
		var results = new Dictionary<string, IEnumerable<ValidationResult>>();
		
		if (configuration.IsRequired)
		{
			var requiredRule = (IValidationRule)Activator.CreateInstance(typeof(RequiredValidationRule));
			SetResults(propertyName, results, requiredRule.Validate(value, propertyName).ToList());
		}

		if (configuration.MaxLength != null)
		{
			var maxLengthRule = (IValidationRule)Activator.CreateInstance(typeof(MaxLengthValidationRule));
			SetResults(propertyName, results, maxLengthRule.Validate(value, propertyName, configuration.MaxLength).ToList());
		}

		if (configuration.MinLength != null)
		{
			var minLengthRule = (IValidationRule)Activator.CreateInstance(typeof(MinLengthValidationRule));
			SetResults(propertyName, results, minLengthRule.Validate(value, propertyName, configuration.MinLength).ToList());
		}

		if (configuration.Regex != null)
		{
			var regexRule = (IValidationRule)Activator.CreateInstance(typeof(RegexValidationRule));
			SetResults(propertyName, results, regexRule.Validate(value, propertyName, configuration.Regex).ToList());
		}
		
		if (configuration.CustomValidators.Any())
		{
			foreach (var customValidatorType in configuration.CustomValidators)
			{
				var customValidator = _ruleResolver(customValidatorType);
				SetResults(propertyName, results, customValidator.Validate(value, propertyName, context).ToList());
			}
		}

		return results;
	}

	private void SetResults(string propertyName, Dictionary<string, IEnumerable<ValidationResult>> results,
		List<ValidationResult> validationResults)
	{
		if (validationResults.Any())
		{
			if (results.TryGetValue(propertyName, out IEnumerable<ValidationResult> existingResults))
			{
				validationResults.AddRange(existingResults);
				results[propertyName] = validationResults;
			}
			else
			{
				results.Add(propertyName, validationResults);
			}
		}
	}
	
	public void RegisterValidationProfile<TProfile>() where TProfile : ValidationProfile, new()
	{
		_validationProfiles.Add(new TProfile());
	}
}