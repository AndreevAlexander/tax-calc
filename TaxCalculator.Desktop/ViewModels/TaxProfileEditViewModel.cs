using System;
using ReactiveUI;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Desktop.ViewModels.BaseTypes;

namespace TaxCalculator.Desktop.ViewModels;

public class TaxProfileEditViewModel : RoutedViewModel
{
    public TaxProfileModel TaxProfile { get; set; }

    public TaxProfileEditViewModel()
    {
        
    }
}