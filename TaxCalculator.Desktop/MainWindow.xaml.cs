using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Desktop.Helpers.ViewFactory;
using TaxCalculator.Desktop.Services.Contracts;
using TaxCalculator.Desktop.Views.Common.Layouts;
using TaxCalculator.Desktop.Views.TaxProfileManagement.TaxProfileGridView;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(INavigationService navigationService)
        {
            InitializeComponent();
            
            Content = navigationService.RootFrame;
            navigationService.NavigateTo<Layout>();
        }
    }
}