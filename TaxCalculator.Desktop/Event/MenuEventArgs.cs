using Avalonia.Interactivity;
using TaxCalculator.Desktop.Controls;

namespace TaxCalculator.Desktop.Event;

public class MenuEventArgs : RoutedEventArgs
{
    public NavigationMenuItem MenuItem { get; }

    public MenuEventArgs(RoutedEvent routedEvent, NavigationMenuItem menuItem) : base(routedEvent)
    {
        MenuItem = menuItem;
    }
}