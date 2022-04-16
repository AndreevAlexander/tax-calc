using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;
using TaxCalculator.Validation.Rules;

namespace TaxCalculator.Validation;

public class ValidationEngine : IValidationEngine
{
	private readonly IServiceProvider _serviceProvider;
	private readonly List<IValidationProfile> _validationProfiles;

	public ValidationEngine(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
		_validationProfiles = new List<IValidationProfile>();
	}

	public ValidationResultContainer Validate<TModel>(TModel model, object? context = null)
	{
		var results = new List<ValidationResult>();

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

				var resultsForProperty = ExecuteValidationRules(propertyValidationConfiguration, propertyNameForValidation, value, context);
				results.AddRange(resultsForProperty);
			}
		}
		
		return new ValidationResultContainer(results);
	}

	private IEnumerable<ValidationResult> ExecuteValidationRules(RuleConfiguration configuration, string propertyName, object? value, object? context = null)
	{
		var results = new List<ValidationResult>();
		
		if (configuration.IsRequired)
		{
			var requiredRule = (IValidationRule)Activator.CreateInstance(typeof(RequiredValidationRule));
			results.AddRange(requiredRule.Validate(value, propertyName));
		}

		if (configuration.MaxLength != null)
		{
			var requiredRule = (IValidationRule)Activator.CreateInstance(typeof(MaxLengthValidationRule));
			results.AddRange(requiredRule.Validate(value, propertyName, configuration.MaxLength));
		}

		if (configuration.MinLength != null)
		{
			var requiredRule = (IValidationRule)Activator.CreateInstance(typeof(MinLengthValidationRule));
			results.AddRange(requiredRule.Validate(value, propertyName, configuration.MinLength));
		}

		if (configuration.Regex != null)
		{
			var requiredRule = (IValidationRule)Activator.CreateInstance(typeof(RegexValidationRule));
			results.AddRange(requiredRule.Validate(value, propertyName, configuration.Regex));
		}
		
		if (configuration.CustomValidators.Any())
		{
			foreach (var customValidatorType in configuration.CustomValidators)
			{
				var customValidator = (IValidationRule)ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, customValidatorType);
				results.AddRange(customValidator.Validate(value, propertyName, context));
			}
		}

		return results;
	}

	public void RegisterValidationProfile(ValidationProfile validationProfile)
	{
		_validationProfiles.Add(validationProfile);
	}
}