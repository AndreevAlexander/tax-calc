namespace TaxCalculator.Desktop.Helpers.ViewFactory;

public interface IViewFactory<TView> where TView : class
{
    TView Create();
}