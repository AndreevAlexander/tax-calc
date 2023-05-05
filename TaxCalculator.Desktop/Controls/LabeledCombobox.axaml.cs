using System.Collections;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace TaxCalculator.Desktop.Controls;

public class LabeledCombobox : TemplatedControl
{
    private string _label;

    private IEnumerable _items;

    private object _selectedItem;
    
    public string Label
    {
        get => _label;
        set => SetAndRaise(LabelProperty, ref _label, value);
    }

    public IEnumerable Items
    {
        get => _items;
        set => SetAndRaise(ItemsProperty, ref _items, value);
    }

    public object SelectedItem
    {
        get => _selectedItem;
        set => SetAndRaise(SelectedItemProperty, ref _selectedItem, value);
    }
    
    public static readonly DirectProperty<LabeledCombobox, string> LabelProperty =
        AvaloniaProperty.RegisterDirect<LabeledCombobox, string>(nameof(Label), comboBox => comboBox.Label,
            (comboBox, value) => comboBox.Label = value);

    public static readonly DirectProperty<LabeledCombobox, IEnumerable> ItemsProperty =
        AvaloniaProperty.RegisterDirect<LabeledCombobox, IEnumerable>(nameof(Items), c => c.Items,
            (c, v) => c.Items = v);

    public static readonly DirectProperty<LabeledCombobox, object> SelectedItemProperty =
        AvaloniaProperty.RegisterDirect<LabeledCombobox, object>(nameof(SelectedItem), c => c.SelectedItem,
            (c, v) => c.SelectedItem = v);
}