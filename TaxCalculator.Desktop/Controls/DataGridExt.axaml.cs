using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using DynamicData;
using TaxCalculator.Desktop.Attributes;

namespace TaxCalculator.Desktop.Controls;

public class DataGridExt : TemplatedControl
{
    private IEnumerable _items;

    private bool _sortable;

    private bool _resizeableColumns;

    private object _selectedItem;

    private DataGrid _dataGrid;

    public IEnumerable Items
    {
        get => _items;
        set => SetAndRaise(ItemsProperty, ref _items, value);
    }

    public bool Sortable
    {
        get => _sortable;
        set => SetAndRaise(SortableProperty, ref _sortable, value);
    }

    public bool ResizeableColumns
    {
        get => _resizeableColumns;
        set => SetAndRaise(ResizeableColumnsProperty, ref _resizeableColumns, value);
    }

    public object SelectedItem
    {
        get => _selectedItem;
        set => SetAndRaise(SelectedItemProperty, ref _selectedItem, value);
    }
    
    public static readonly DirectProperty<DataGridExt, IEnumerable> ItemsProperty =
        AvaloniaProperty.RegisterDirect<DataGridExt, IEnumerable>(nameof(Items), g => g.Items,
            (g, value) => g.Items = value);

    public static readonly DirectProperty<DataGridExt, bool> SortableProperty =
        AvaloniaProperty.RegisterDirect<DataGridExt, bool>(nameof(Sortable), g => g.Sortable,
            (g, value) => g.Sortable = value);

    public static readonly DirectProperty<DataGridExt, bool> ResizeableColumnsProperty =
        AvaloniaProperty.RegisterDirect<DataGridExt, bool>(nameof(ResizeableColumns), g => g.ResizeableColumns,
            (g, value) => g.ResizeableColumns = value);

    public static readonly DirectProperty<DataGridExt, object> SelectedItemProperty =
        AvaloniaProperty.RegisterDirect<DataGridExt, object>(nameof(SelectedItem), g => g.SelectedItem,
            (g, value) => g.SelectedItem = value);

    public DataGridExt()
    {
        DataGrid.ItemsProperty.Changed.AddClassHandler<DataGrid>(OnItemsPropertyChanged);
    }

    private void OnItemsPropertyChanged(DataGrid sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (_dataGrid != null)
        {
            var columns = GenerateColumns().ToList();
            if (columns.Any())
            {
                _dataGrid.Columns.AddRange(columns);
            }
        }
    }
    
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        _dataGrid = e.NameScope.Find<DataGrid>("PART_DataGridExt");
    }

    private IEnumerable<DataGridColumn> GenerateColumns()
    {
        var result = new List<DataGridColumn>();
        
        if (Items != null)
        {
            var item = Items.Cast<object>().FirstOrDefault();
            if (item != null)
            {
                var modelType = item.GetType();
                var properties = modelType.GetProperties();

                foreach (var propertyInfo in properties)
                {
                    var column = GenerateColumn(propertyInfo);
                    if (column != null)
                    {
                        result.Add(column);
                    }
                }
            }
        }

        return result;
    }
    
    private DataGridColumn GenerateColumn(PropertyInfo propertyInfo)
    {
        DataGridColumn column = null;
        
        var columnAttribute = propertyInfo.GetCustomAttribute<GridColumnAttribute>();
        if (columnAttribute != null)
        {
            if (string.IsNullOrEmpty(columnAttribute.NestedPropertyPath))
            {
                column = CreateColumn(propertyInfo, columnAttribute.DisplayName, propertyInfo.Name);
            }
            else
            {
                var nestedProperty = propertyInfo;
                var nestedPropertyNames = columnAttribute.NestedPropertyPath.Split('.');
                if (nestedPropertyNames.Length > 1) 
                {
                    for (var i = 1; i < nestedPropertyNames.Length; i++)
                    {
                        nestedProperty = nestedProperty.PropertyType.GetProperty(nestedPropertyNames[i]);
                    }
                }

                var bindPath = $"{propertyInfo.Name}.{columnAttribute.NestedPropertyPath}";
                column = CreateColumn(nestedProperty, columnAttribute.DisplayName, bindPath);
            }

            column.DisplayIndex = columnAttribute.DisplayIndex;
        }
        
        return column;
    }

    private DataGridColumn CreateColumn(PropertyInfo propertyInfo, string displayName, string bindPath)
    {
        displayName = displayName ?? propertyInfo.Name;
        return propertyInfo.PropertyType == typeof(bool)
        ? new DataGridCheckBoxColumn
                { Header = displayName, Binding = new Binding(bindPath) }
        : new DataGridTextColumn
                { Header = displayName, Binding = new Binding(bindPath) };
    }
}