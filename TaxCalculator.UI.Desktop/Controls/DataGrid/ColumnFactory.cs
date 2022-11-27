using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace TaxCalculator.UI.Desktop.Controls.DataGrid
{
    public class ColumnFactory : IColumnFactory
    {
        public DataGridColumn CreateColumn(PropertyInfo property)
        {
            var columnAttribute = property.GetCustomAttribute<GeneratedColumnAttribute>();
            if (columnAttribute == null)
            {
                throw new Exception($"[GeneratedColumn] should be applied to property {property.Name}");
            }

            var result = property.PropertyType == typeof(bool)
                ? (DataGridBoundColumn)Create<DataGridCheckBoxColumn>(columnAttribute, property.Name)
                : Create<DataGridTextColumn>(columnAttribute, property.Name);

            return result;
        }

        private TColumn Create<TColumn>(GeneratedColumnAttribute attribute, string propertyName) where TColumn : DataGridBoundColumn, new()
        {
            var column = new TColumn
            {
                Header = attribute.DisplayName,
                IsReadOnly = attribute.IsReadOnly,
                Binding = new Binding
                {
                    Path = new PropertyPath(propertyName),
                    Mode = BindingMode.TwoWay
                }
            };

            return column;
        }
    }
}
