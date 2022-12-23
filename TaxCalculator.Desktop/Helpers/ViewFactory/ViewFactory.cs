using System;

namespace TaxCalculator.Desktop.Helpers.ViewFactory;

public class ViewFactory<TView> : IViewFactory<TView> where TView : class
{
    private readonly Func<TView> _factoryFunction;

    public ViewFactory(Func<TView> factoryFunction)
    {
        _factoryFunction = factoryFunction;
    }
    
    public TView Create()
    {
        return _factoryFunction();
    }
}