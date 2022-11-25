using System;
using System.Collections.Generic;

namespace TaxCalculator.Validation.Contracts
{
    public interface IValidationProfile
    {
        void ForModel<TModel>(Action<IRuleBuilder<TModel>> builder) where TModel : class;

        IRuleBuilder<TModel> GetRuleBuilder<TModel>() where TModel : class;

        IEnumerable<Type> GetModelTypes();
    }
}