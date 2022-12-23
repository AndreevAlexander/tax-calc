using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TaxCalculator.Desktop.Mvvm.ModelContainer;

public class ModelContainer<TModel> : DynamicObject, INotifyPropertyChanged
    where TModel : class
{
    public event PropertyChangedEventHandler PropertyChanged;

    public Type Type { get; }

    public bool IsDirty => !_initialValues.SetEquals(_values);

    private readonly IDictionary<string, object> _values;

    private readonly HashSet<KeyValuePair<string, object>> _initialValues;

    private readonly object _model;

    public ModelContainer(TModel model)
    {
        _model = model;
        Type = typeof(TModel);

        _initialValues = new HashSet<KeyValuePair<string, object>>();
        InitializeContainer();

        _values = _initialValues.ToDictionary(x => x.Key, x => x.Value);
    }

    private void InitializeContainer()
    {
        var properties = _model.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public);

        foreach (var property in properties)
        {
            var propertyValue = property.GetValue(_model);
            var propertyName = property.Name;

            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                var value = propertyValue != null ? CreateInnerContainer(propertyValue) : null;
                _initialValues.Add(new KeyValuePair<string, object>(propertyName, value));
            }
            else
            {
                _initialValues.Add(new KeyValuePair<string, object>(propertyName, propertyValue));
            }
        }
    }

    private object CreateInnerContainer(object innerModel)
    {
        var innerContainerType = typeof(ModelContainer<>).MakeGenericType(innerModel.GetType());
        return Activator.CreateInstance(innerContainerType, new object[] { innerModel });
    }

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        result = this[binder.Name];
        return result != null;
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        this[binder.Name] = value;
        return true;
    }

    public void Reset()
    {
        _values.Clear();

        foreach (var initialValue in _initialValues)
        {
            this[initialValue.Key] = initialValue.Value;
        }
    }

    public object this[string key]
    {
        get
        {
            _values.TryGetValue(key, out object result);
            return result;
        }
        set
        {
            _values[key] = value;
            RaisePropertyChanged(key);
        }
    }
}