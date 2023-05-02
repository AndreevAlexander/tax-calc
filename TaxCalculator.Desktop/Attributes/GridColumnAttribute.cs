using System;

namespace TaxCalculator.Desktop.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class GridColumnAttribute : Attribute
{
    public string DisplayName { get; }

    public string NestedPropertyPath { get; }

    public int DisplayIndex { get; }

    public GridColumnAttribute(string displayName = null, string nestedPropertyPath = null, int displayIndex = -1)
    {
        DisplayName = displayName;
        NestedPropertyPath = nestedPropertyPath;
        DisplayIndex = displayIndex;
    }
}