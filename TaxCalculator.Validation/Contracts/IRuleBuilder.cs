using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TaxCalculator.Validation.Contracts;

public interface IRuleBuilder
{
    
}

public interface IRuleBuilder<TModel> : IRuleBuilder where TModel : class
{ 
    IRuleStage Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression);

    IEnumerable<RuleConfiguration> Build();
}