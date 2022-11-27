// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using TaxCalculator.UI.Desktop.Attributes;
using TaxCalculator.UI.Desktop.Extensions;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TaxCalculator.UI.Desktop.Views.TaxProfiles
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [NavigationDetails("Tax Profiles", "\xE74C")]
    public sealed partial class TaxProfileView : Page
    {
        public TaxProfileView()
        {
            this.InitializeComponent();

            DataContext = Application.Current.GetViewModel<TaxProfileViewModel>();
        }
    }
}