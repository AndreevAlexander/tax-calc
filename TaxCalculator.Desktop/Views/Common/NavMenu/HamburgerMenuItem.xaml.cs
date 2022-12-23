using System.Windows;
using System.Windows.Controls;

namespace TaxCalculator.Desktop.Views.Common.NavMenu;

public partial class HamburgerMenuItem : UserControl
{
    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public string Glyph
    {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(LabelProperty, value);
    }

    public static DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label),
        typeof(string),
        typeof(HamburgerMenuItem),
        new PropertyMetadata(null));
    
    public static DependencyProperty GlyphProperty = DependencyProperty.Register(nameof(Glyph),
        typeof(string),
        typeof(HamburgerMenuItem),
        new PropertyMetadata(null));

    public HamburgerMenuItem()
    {
        InitializeComponent();
    }
}