using System;
using System.Collections.Generic;
using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.Validation
{
    public abstract class ValidationProfile : IValidationProfile
    {
        protected readonly Dictionary<Type, IRuleBuilder> RulesForModels;

        public ValidationProfile()
        {
            RulesForModels = new Dictionary<Type, IRuleBuilder>();
        }

        public void ForModel<TModel>(Action<IRuleBuilder<TModel>> builder) where TModel : class
        {
            var ruleBuilder = new RuleBuilder<TModel>();
            builder(ruleBuilder);

            if (!RulesForModels.ContainsKey(typeof(TModel)))
            {
                RulesForModels.Add(typeof(TModel), ruleBuilder);
            }
        }

        public IRuleBuilder<TModel> GetRuleBuilder<TModel>() where TModel : class
        {
            return (IRuleBuilder<TModel>)RulesForModels[typeof(TModel)];
        }

        public IEnumerable<Type> GetModelTypes()
        {
            return RulesForModels.Keys;
        }
    }
}