using System.Windows;
using System.Windows.Controls;

namespace TaxCalculator.Desktop.Views.Common.NavMenu;

public partial class HamburgerMenu : UserControl
{
    public UIElementCollection NavigationItems
    {
        get => (UIElementCollection)GetValue(NavigationItemsProperty);
        set => SetValue(NavigationItemsProperty, value);
    }

    public static readonly DependencyProperty NavigationItemsProperty = DependencyProperty.Register(nameof(NavigationItems),
        typeof(UIElementCollection),
        typeof(HamburgerMenu),
        new PropertyMetadata());
    
    public HamburgerMenu()
    {
        InitializeComponent();
        NavigationItems = MenuSideBar.Children;
    }
}