using System;
using System.Collections.Generic;
using System.Reflection;

namespace TaxCalculator.Validation;

public class RuleConfiguration
{
    private PropertyInfo PropertyInfo { get; }

    public string PropertyName { get; set; }
    
    public bool IsRequired { get; set; }
	
    public int? MaxLength { get; set; }
	
    public int? MinLength { get; set; }
	
    public string? Regex { get; set; }

    public bool IsNumeric { get; set; }
    
    public List<Type> CustomValidators { get; }

    public RuleConfiguration(PropertyInfo propertyInfo)
    {
        PropertyInfo = propertyInfo;
        PropertyName = propertyInfo.Name;
        CustomValidators = new List<Type>();
        MaxLength = null;
        MinLength = null;
        Regex = null;
    }

    public object? GetValue(object model)
    {
        return PropertyInfo.GetValue(model);
    }
}
