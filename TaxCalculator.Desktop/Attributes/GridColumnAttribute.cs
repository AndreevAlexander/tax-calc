using System;

namespace TaxCalculator.Desktop.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class GridColumnAttribute : Attribute
{
    public string DisplayName { get; }
    
    public int? Index { get; }

    public GridColumnAttribute(string displayName, int? index = null)
    {
        DisplayName = displayName;
        Index = index;
    }
}