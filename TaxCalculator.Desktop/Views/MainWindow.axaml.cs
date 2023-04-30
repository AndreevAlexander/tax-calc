using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TaxCalculator.Desktop.Controls;
using TaxCalculator.Desktop.Event;
using TaxCalculator.Desktop.ViewModels;

namespace TaxCalculator.Desktop.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}