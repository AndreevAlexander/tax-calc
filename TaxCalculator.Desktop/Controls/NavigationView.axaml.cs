using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace TaxCalculator.Desktop.Controls;

public class NavigationView : ItemsControl
{
    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public static AvaloniaProperty<object> ContentProperty =
        AvaloniaProperty.Register<NavigationView, object>(nameof(Content));
}