using System.Collections;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace TaxCalculator.Desktop.Controls;

public class CrudDataGrid : TemplatedControl
{
    private string _title;

    private ICommand _editCommand;

    private ICommand _removeCommand;

    private object _selectedItems;

    private IEnumerable _items;
    
    private bool _sortable;

    private bool _resizeableColumns;

    public string Title
    {
        get => _title;
        set => SetAndRaise(TitleProperty, ref _title, value);
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

    public object SelectedItem
    {
        get => _selectedItems;
        set => SetAndRaise(SelectedItemProperty, ref _selectedItems, value);
    }

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

    public static readonly DirectProperty<CrudDataGrid, string> TitleProperty =
        AvaloniaProperty.RegisterDirect<CrudDataGrid, string>(nameof(Title),
            grid => grid.Title,
            (grid, value) => grid.Title = value);

    public static readonly DirectProperty<CrudDataGrid, ICommand> EditCommandProperty =
        AvaloniaProperty.RegisterDirect<CrudDataGrid, ICommand>(nameof(EditCommand),
            grid => grid.EditCommand,
            (grid, c) => grid.EditCommand = c);

    public static readonly DirectProperty<CrudDataGrid, ICommand> RemoveCommandProperty =
        AvaloniaProperty.RegisterDirect<CrudDataGrid, ICommand>(nameof(RemoveCommand),
            grid => grid.RemoveCommand,
            (grid, c) => grid.RemoveCommand = c);

    public static readonly DirectProperty<CrudDataGrid, object> SelectedItemProperty =
        AvaloniaProperty.RegisterDirect<CrudDataGrid, object>(nameof(SelectedItem),
            grid => grid.SelectedItem, 
            (grid, i) => grid.SelectedItem = i);

    public static readonly DirectProperty<CrudDataGrid, IEnumerable> ItemsProperty =
        AvaloniaProperty.RegisterDirect<CrudDataGrid, IEnumerable>(nameof(Items), grid => grid.Items,
            (grid, i) => grid.Items = i);
    
    public static readonly DirectProperty<CrudDataGrid, bool> SortableProperty =
        AvaloniaProperty.RegisterDirect<CrudDataGrid, bool>(nameof(Sortable), g => g.Sortable,
            (g, value) => g.Sortable = value);

    public static readonly DirectProperty<CrudDataGrid, bool> ResizeableColumnsProperty =
        AvaloniaProperty.RegisterDirect<CrudDataGrid, bool>(nameof(ResizeableColumns), g => g.ResizeableColumns,
            (g, value) => g.ResizeableColumns = value);
}