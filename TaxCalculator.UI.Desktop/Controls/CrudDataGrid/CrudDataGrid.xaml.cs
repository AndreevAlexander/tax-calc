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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using TaxCalculator.UI.Desktop.Controls.DataGrid;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TaxCalculator.UI.Desktop.Controls.CrudDataGrid
{
    public sealed partial class CrudDataGrid : UserControl
    {
        public ICommand AddButtonCommand
        {
            get => (ICommand)GetValue(AddCommandDependencyProperty);
            set => SetValue(AddCommandDependencyProperty, value);
        }

        public static DependencyProperty AddCommandDependencyProperty = DependencyProperty.Register(nameof(AddButtonCommand), 
            typeof(ICommand),
            typeof(CrudDataGrid),
            new PropertyMetadata(null));

        public ICollection Items
        {
            get => (ICollection)GetValue(ItemsDependencyProperty);
            set => SetValue(ItemsDependencyProperty, value);
        }

        public static DependencyProperty ItemsDependencyProperty = DependencyProperty.Register(nameof(Items),
            typeof(ICollection),
            typeof(CrudDataGrid),
            new PropertyMetadata(null));

        public CrudDataGrid()
        {
            this.InitializeComponent();
        }
    }
}
