using System;
using System.Drawing;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace TaxCalculator.Desktop.Controls;

public class DataGridComboboxColumn : DataGridBoundColumn
{
    private readonly Array _comboBoxItems;

    public DataGridComboboxColumn(Array comboBoxItems)
    {
        _comboBoxItems = comboBoxItems;
        BindingTarget = ComboBox.SelectedItemProperty;
    }
    
    protected override IControl GenerateElement(DataGridCell cell, object dataItem)
    {
        var comboBox = new ComboBox
        {
            Name = "CellComboBox", 
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Items = _comboBoxItems
        };
        
        if (Binding != null)
        {
            comboBox.Bind(ComboBox.SelectedItemProperty, Binding);
        }

        return comboBox;
    }

    protected override object PrepareCellForEdit(IControl editingElement, RoutedEventArgs editingEventArgs)
    {
        /*if (editingElement is ComboBox comboBox)
        {
            comboBox.Items = _comboBoxItems;
        }*/

        return null;
    }

    protected override IControl GenerateEditingElementDirect(DataGridCell cell, object dataItem)
    {
        var comboBox = new ComboBox
        {
            IsDropDownOpen = true, 
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Items = _comboBoxItems
        };

        return comboBox;
    }
}