using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using TaxCalculator.UI.MVVM;

namespace TaxCalculator.UI.Desktop.Extensions
{
    public static class ApplicationExtensions
    {
        public static TViewModel GetViewModel<TViewModel>(this Application application) where TViewModel : BaseViewModel
        {
            var container = ((App)application).Container;
            return (TViewModel)ActivatorUtilities.GetServiceOrCreateInstance(container, typeof(TViewModel));
        }

        public static INavigator GetNavigator(this Application application)
        {
            return ((App)application).Navigator;
        }

        public static TService GetService<TService>(this Application application)
        {
            return ((App)application).Container.GetService<TService>();
        }
    }
}
