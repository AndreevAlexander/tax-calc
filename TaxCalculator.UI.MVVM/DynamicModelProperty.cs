using System;
using System.Reflection;
using Microsoft.UI.Xaml.Data;

namespace TaxCalculator.UI.MVVM
{
    public class DynamicModelProperty : ICustomProperty
    {
        public bool CanRead { get; }
        
        public bool CanWrite { get; }
        
        public string Name { get; }
        
        public Type Type { get; }

        public DynamicModelProperty(PropertyInfo propertyInfo)
        {
            Type = propertyInfo.PropertyType;
            CanRead = propertyInfo.CanRead;
            CanWrite = propertyInfo.CanWrite;
            Name = propertyInfo.Name;
        }

        public object GetValue(object target)
        {
            return ((ModelContainer)target)[Name];
        }

        public void SetValue(object target, object value)
        {
            ((ModelContainer)target)[Name] = value;
        }

        public object GetIndexedValue(object target, object index)
        {
            return ((ModelContainer)target)[index.ToString()];
        }

        public void SetIndexedValue(object target, object value, object index)
        {
            ((ModelContainer)target)[index.ToString()] = value;
        }
    }
}
