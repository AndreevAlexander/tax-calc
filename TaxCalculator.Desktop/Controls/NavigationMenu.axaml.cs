using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TaxCalculator.Desktop.Event;

namespace TaxCalculator.Desktop.Controls;

public class NavigationMenu : ItemsControl
{
    private NavigationMenuItem _selectedItem;

    public event EventHandler<MenuEventArgs> SelectionChanged
    {
        add => AddHandler(SelectionChangedEvent, value);
        remove => RemoveHandler(SelectionChangedEvent, value);
    }

    public static readonly RoutedEvent<MenuEventArgs> SelectionChangedEvent =
        RoutedEvent.Register<NavigationMenu, MenuEventArgs>(nameof(SelectionChanged), RoutingStrategies.Bubble);

    public NavigationMenu()
    {
        NavigationMenuItem.IsSelectedProperty.Changed.AddClassHandler<NavigationMenuItem>(OnNavigationMenuItemSelected);
    }

    public void OnNavigationMenuItemSelected(NavigationMenuItem sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (!sender.Equals(_selectedItem))
        {
            if (_selectedItem != null)
            {
                _selectedItem.IsSelected = false;
            }

            _selectedItem = sender;
        }
        
        RaiseEvent(new MenuEventArgs(SelectionChangedEvent, sender));
    }
}