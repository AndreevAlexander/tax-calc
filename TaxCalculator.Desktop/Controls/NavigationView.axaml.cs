using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using ReactiveUI;
using TaxCalculator.Desktop.Event;

namespace TaxCalculator.Desktop.Controls;

public class NavigationView : ItemsControl
{
    private bool _isPaneOpen;

    private int _openPaneLength;

    private int _compactPaneLength;

    private ICommand _itemClickCommand;
    
    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
    
    public bool IsPaneOpen
    {
        get => _isPaneOpen;
        set => SetAndRaise(IsPaneOpenProperty, ref _isPaneOpen, value);
    }

    public int OpenPaneLength
    {
        get => _openPaneLength;
        set => SetAndRaise(OpenPaneLengthProperty, ref _openPaneLength, value);
    }

    public int CompactPaneLength
    {
        get => _compactPaneLength;
        set => SetAndRaise(CompactPaneLengthProperty, ref _compactPaneLength, value);
    }

    public ICommand ItemClickCommand
    {
        get => _itemClickCommand;
        set => SetAndRaise(ItemClickCommandProperty, ref _itemClickCommand, value);
    }

    public event EventHandler<MenuEventArgs> SelectionChanged
    {
        add => AddHandler(SelectionChangedEvent, value);
        remove => RemoveHandler(SelectionChangedEvent, value);
    }

    public static AvaloniaProperty<object> ContentProperty =
        AvaloniaProperty.Register<NavigationView, object>(nameof(Content));
    
    public static readonly DirectProperty<NavigationView, bool> IsPaneOpenProperty =
        AvaloniaProperty.RegisterDirect<NavigationView, bool>(nameof(IsPaneOpen), view => view.IsPaneOpen,
            (view, value) => view.IsPaneOpen = value);

    public static readonly DirectProperty<NavigationView, int> OpenPaneLengthProperty =
        AvaloniaProperty.RegisterDirect<NavigationView, int>(nameof(OpenPaneLength), view => view.OpenPaneLength,
            (view, value) => view.OpenPaneLength = value);

    public static readonly DirectProperty<NavigationView, int> CompactPaneLengthProperty =
        AvaloniaProperty.RegisterDirect<NavigationView, int>(nameof(CompactPaneLength), view => view.CompactPaneLength,
            (view, value) => view.CompactPaneLength = value);

    public static readonly DirectProperty<NavigationView, ICommand> ItemClickCommandProperty =
        AvaloniaProperty.RegisterDirect<NavigationView, ICommand>(nameof(ItemClickCommand),
            view => view.ItemClickCommand, (view, command) => view.ItemClickCommand = command);
    
    public static readonly RoutedEvent<MenuEventArgs> SelectionChangedEvent =
        RoutedEvent.Register<NavigationMenu, MenuEventArgs>(nameof(SelectionChanged), RoutingStrategies.Bubble);

    public NavigationView()
    {
        NavigationMenuItem.IsSelectedProperty.Changed.AddClassHandler<NavigationMenuItem>(OnSelectionChanged);
    }

    public virtual void OnSelectionChanged(NavigationMenuItem sender, AvaloniaPropertyChangedEventArgs e)
    {
        RaiseEvent(new MenuEventArgs(SelectionChangedEvent, sender));

        if (ItemClickCommand != null && ItemClickCommand.CanExecute(sender))
        {
            ItemClickCommand.Execute(sender);
        }
    }
}