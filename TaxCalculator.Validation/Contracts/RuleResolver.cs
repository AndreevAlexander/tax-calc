using System;

namespace TaxCalculator.Validation.Contracts
{
    public delegate TValidationRule RuleResolver<out TValidationRule>(Type ruleType) where TValidationRule : IValidationRuleBase;
}