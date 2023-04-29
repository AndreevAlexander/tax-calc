using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace TaxCalculator.Desktop.Controls;

[PseudoClasses(":selected")]
public class NavigationMenuItem : TemplatedControl
{
    private const string SelectedPseudoClass = ":selected";

    private string _title;
    
    private Geometry _iconData;

    private int _iconWidth;

    private int _iconHeight;

    private bool _isSelected;

    public string Title
    {
        get => _title;
        set => SetAndRaise(TitleProperty, ref _title, value);
    }
    
    public Geometry IconData
    {
        get => _iconData;
        set => SetAndRaise(IconDataProperty, ref _iconData, value);
    }

    public int IconWidth
    {
        get => _iconWidth;
        set => SetAndRaise(IconWidthProperty, ref _iconWidth, value);
    }

    public int IconHeight
    {
        get => _iconHeight;
        set => SetAndRaise(IconHeightProperty, ref _iconHeight, value);
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetAndRaise(IsSelectedProperty, ref _isSelected, value);
    }
    
    public event EventHandler<RoutedEventArgs> Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    public static readonly DirectProperty<NavigationMenuItem, string> TitleProperty =
        AvaloniaProperty.RegisterDirect<NavigationMenuItem, string>(nameof(Title), i => i.Title,
            (i, v) => i.Title = v);

    public static readonly DirectProperty<NavigationMenuItem, Geometry> IconDataProperty =
        AvaloniaProperty.RegisterDirect<NavigationMenuItem, Geometry>(nameof(IconData), i => i.IconData,
            (i, v) => i.IconData = v);

    public static readonly DirectProperty<NavigationMenuItem, int> IconWidthProperty =
        AvaloniaProperty.RegisterDirect<NavigationMenuItem, int>(nameof(IconWidth), i => i.IconWidth,
            (i, v) => i.IconWidth = v);

    public static readonly DirectProperty<NavigationMenuItem, int> IconHeightProperty =
        AvaloniaProperty.RegisterDirect<NavigationMenuItem, int>(nameof(IconHeight), i => i.IconHeight,
            (i, v) => i.IconHeight = v);

    public static readonly DirectProperty<NavigationMenuItem, bool> IsSelectedProperty =
        AvaloniaProperty.RegisterDirect<NavigationMenuItem, bool>(nameof(IsSelected), i => i.IsSelected,
            (i, v) => i.IsSelected = v);

    public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
        RoutedEvent.Register<NavigationMenuItem, RoutedEventArgs>(nameof(Click), RoutingStrategies.Bubble);

    public NavigationMenuItem()
    {
        ClickEvent.AddClassHandler<NavigationMenuItem>((s, e) => s.OnNavigationMenuItemClick(s, e));
    }
    
    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(ClickEvent));    
    }

    public virtual void OnNavigationMenuItemClick(NavigationMenuItem sender, RoutedEventArgs e)
    {
        sender.IsSelected = !sender.IsSelected;

        if (sender.IsSelected)
        {
            sender.PseudoClasses.Add(SelectedPseudoClass);
        }
        else
        {
            sender.PseudoClasses.Remove(SelectedPseudoClass);
        }

        e.Handled = true;
    }
}