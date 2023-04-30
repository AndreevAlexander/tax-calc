using System.Linq;
using System.Reflection;
using ReactiveUI;
using TaxCalculator.Desktop.ViewModels;

namespace TaxCalculator.Desktop;

public class Bootstrapper
{
    public IContainer Container { get; }
    
    public Bootstrapper(IContainer container)
    {
        Container = container;
    }
    
    public void BootstrapServices()
    {
        RegisterDatabase();
        RegisterServices();
        RegisterViewModels();
    }

    private void RegisterDatabase()
    {
        
    }

    private void RegisterServices()
    {
        /*Container.Register<IMyService, MyService>();
        Container.Register<OtherService>();*/
    }

    private void RegisterViewModels()
    {
        Container.Register<IScreen, MainWindowViewModel>();
        
        var viewModelTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => x.BaseType != null && x.BaseType == typeof(ViewModelBase) && x != typeof(MainWindowViewModel))
            .ToArray();

        foreach (var viewModelType in viewModelTypes)
        {
            Container.Register(viewModelType);
        }
    }
}