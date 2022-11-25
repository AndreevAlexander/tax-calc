using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace TaxCalculator.UI.Desktop
{
    public interface INavigator
    {
        void NavigateTo<TView>(object parameters) where TView : Page;

        Page GetCurrentView();

        void RegisterMenuItem<TView>() where TView : Page;
    }
}
