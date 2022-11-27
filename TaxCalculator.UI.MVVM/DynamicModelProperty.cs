using System;
using System.Reflection;
using Microsoft.UI.Xaml.Data;

namespace TaxCalculator.UI.MVVM
{
    public class DynamicModelProperty<TOwner> : ICustomProperty where TOwner : class
    {
        private readonly ModelContainer<TOwner> _owner;

        public bool CanRead { get; }
        
        public bool CanWrite { get; }
        
        public string Name { get; }
        
        public Type Type { get; }

        public DynamicModelProperty(PropertyInfo propertyInfo, ModelContainer<TOwner> owner)
        {
            _owner = owner;
            Type = propertyInfo.PropertyType;
            CanRead = propertyInfo.CanRead;
            CanWrite = propertyInfo.CanWrite;
            Name = propertyInfo.Name;
        }

        public object GetValue(object target)
        {
            return ((dynamic)_owner)[Name];
        }

        public void SetValue(object target, object value)
        {
            ((dynamic)_owner)[Name] = value;
        }

        public object GetIndexedValue(object target, object index)
        {
            return ((dynamic)_owner)[index.ToString()];
        }

        public void SetIndexedValue(object target, object value, object index)
        {
            ((dynamic)_owner)[index.ToString()] = value;
        }
    }
}
