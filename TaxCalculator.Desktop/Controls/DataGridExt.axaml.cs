using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using TaxCalculator.Desktop.Attributes;

namespace TaxCalculator.Desktop.Controls;

public class DataGridExt : TemplatedControl
{
    private IEnumerable _items;

    private bool _sortable;

    private bool _resizeableColumns;

    private ICommand _editCommand;

    private ICommand _removeCommand;

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

    public ICommand EditCommand
    {
        get => _editCommand;
        set => SetAndRaise(EditCommandProperty, ref _editCommand, value);
    }

    public ICommand RemoveCommand
    {
        get => _removeCommand;
        set => SetAndRaise(RemoveCommandProperty, ref _removeCommand, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
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

    public static readonly DirectProperty<DataGridExt, ICommand> EditCommandProperty =
        AvaloniaProperty.RegisterDirect<DataGridExt, ICommand>(nameof(EditCommand), g => g.EditCommand,
            (g, value) => g.EditCommand = value);

    public static readonly DirectProperty<DataGridExt, ICommand> RemoveCommandProperty =
        AvaloniaProperty.RegisterDirect<DataGridExt, ICommand>(nameof(RemoveCommand), g => g.RemoveCommand,
            (g, value) => g.RemoveCommand = value);
    
    public static readonly StyledProperty<object> CommandParameterProperty =
        AvaloniaProperty.Register<DataGridExt, object>(nameof(CommandParameter));


    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        //TODO: find a better way to initialize columns
        var dataGrid = e.NameScope.Find<DataGrid>("DataGridExt");

        if (dataGrid != null)
        {
            var columns = GenerateColumns();
            if (columns.Any())
            {
                var actionsColumn = GenerateActionsColumn();
                dataGrid.Columns.Add(actionsColumn);
            }
        }
    }

    private IEnumerable<DataGridBoundColumn> GenerateColumns()
    {
        var result = new List<DataGridBoundColumn>();
        
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
    
    private DataGridBoundColumn GenerateColumn(PropertyInfo propertyInfo)
    {
        DataGridBoundColumn column = null;
        
        var columnAttribute = propertyInfo.GetCustomAttribute<GridColumnAttribute>();
        if (columnAttribute != null)
        {
            column = propertyInfo.PropertyType == typeof(bool)
                ? new DataGridCheckBoxColumn
                    { Header = columnAttribute.DisplayName, Binding = new Binding(propertyInfo.Name) }
                : new DataGridTextColumn
                    { Header = columnAttribute.DisplayName, Binding = new Binding(propertyInfo.Name) };
        }
        
        return column;
    }

    private DataGridTemplateColumn GenerateActionsColumn()
    {
        var actionsColumn = new DataGridTemplateColumn
        {
            CellTemplate = new DataTemplate
            {
                Content = new StackPanel
                {
                    Spacing = 2,
                    Orientation = Orientation.Horizontal,
                    Children =
                    {
                        new Button
                        {
                            Content = "Edit",
                            Command = EditCommand,
                            CommandParameter = CommandParameter
                        },
                        new Button
                        {
                            Content = "Remove",
                            Command = RemoveCommand,
                            CommandParameter = CommandParameter
                        },
                    }
                }
            }
        };

        return actionsColumn;
    }
}