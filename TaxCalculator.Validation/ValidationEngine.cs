﻿using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;
using TaxCalculator.Validation.Rules;

namespace TaxCalculator.Validation;

public class ValidationEngine : IValidationEngine
{
	private readonly RuleResolver _ruleResolver;
	private readonly IProfileProvider _profileProvider;
	private readonly Dictionary<string, IEnumerable<ValidationResult>> _validationResults;

	public ValidationEngine(RuleResolver ruleResolver, IProfileProvider profileProvider)
	{
		_ruleResolver = ruleResolver;
		_profileProvider = profileProvider;
		_validationResults = new();
	}

	public async Task<ValidationResultContainer> ValidateAsync<TModel>(TModel model, object? context = null) where TModel : class
	{
		_validationResults.Clear();

		var profilesForModel = _profileProvider.GetRules<TModel>();
		
		foreach (var validationProfile in profilesForModel)
		{
			var ruleBuilder = validationProfile.GetRuleBuilder<TModel>();
			var ruleConfigurations = ruleBuilder.Build();

			foreach (var ruleConfiguration in ruleConfigurations)
			{
				await ExecuteValidationRulesAsync(ruleConfiguration, model, context);
			}
		}
		
		return new ValidationResultContainer(_validationResults);
	}

	private async Task ExecuteValidationRulesAsync<TModel>(
		RuleConfiguration configuration,
		TModel model,
		object? context = null) where TModel : class
	{
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
	}

	private void SetResults(string propertyName,
							List<ValidationResult> validationResults)
	{
		if (validationResults.Any())
		{
			if (_validationResults.TryGetValue(propertyName, out IEnumerable<ValidationResult>? existingResults))
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