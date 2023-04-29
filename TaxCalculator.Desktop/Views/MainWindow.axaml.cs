using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TaxCalculator.Desktop.Controls;
using TaxCalculator.Desktop.Event;

namespace TaxCalculator.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void NavigationMenu_OnSelectionChanged(object sender, MenuEventArgs e)
    {
        var title = e.MenuItem;

        Console.WriteLine(title);
        
        e.Handled = true;
    }
}