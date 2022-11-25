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
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using TaxCalculator.UI.Desktop.Attributes;
using TaxCalculator.UI.Desktop.Views.TaxProfiles;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TaxCalculator.UI.Desktop
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Window, INavigator
    {
        public Shell()
        {
            this.InitializeComponent();
        }

        private void NavigationView_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            SetCurrentNavigationItem(args.SelectedItemContainer as NavigationViewItem);
        }

        private void NavigationView_OnLoaded(object sender, RoutedEventArgs e)
        {
            var defaultViewTypeName = typeof(TaxProfileView).FullName;
            SetCurrentNavigationItem(GetNavigationItems().FirstOrDefault(x => x.Tag.Equals(defaultViewTypeName)));
        }

        private void SetCurrentNavigationItem(NavigationViewItem item, object parameters = null)
        {
            if (item.Tag == null)
            {
                throw new Exception("Tag should contain page type");
            }

            var viewType = Type.GetType(item.Tag.ToString());
            ContentFrame.Navigate(viewType, parameters ?? item.Content);

            NavigationView.SelectedItem = item;
        }

        private IEnumerable<NavigationViewItem> GetNavigationItems()
        {
            return NavigationView.MenuItems.Select(x => x as NavigationViewItem).ToList();
        }

        public void NavigateTo<TView>(object parameters) where TView : Page
        {
            var navigationViewItem = GetNavigationItems().FirstOrDefault(x => x.Tag.Equals(typeof(Type).FullName));
            SetCurrentNavigationItem(navigationViewItem);
        }

        public Page GetCurrentView()
        {
            return ContentFrame.Content as Page;
        }

        public void RegisterMenuItem<TView>() where TView : Page
        {
            var viewType = typeof(TView);
            var navigationDetails = viewType.GetCustomAttribute<NavigationDetailsAttribute>();
            if (navigationDetails == null)
            {
                throw new Exception($"[NavigationDetails] attribute should be applied to view {viewType.FullName}");
            }

            var navigationViewItem = new NavigationViewItem
            {
                Content = navigationDetails.DisplayName,
                Tag = viewType.FullName,
                Icon = new FontIcon
                {
                    FontFamily = new FontFamily(navigationDetails.Font),
                    Glyph = navigationDetails.Glyph
                }
            };

            NavigationView.MenuItems.Add(navigationViewItem);
        }
    }
}
