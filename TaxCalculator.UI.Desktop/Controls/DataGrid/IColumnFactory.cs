using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.WinUI.UI.Controls;

namespace TaxCalculator.UI.Desktop.Controls.DataGrid
{
    public interface IColumnFactory
    {
        DataGridColumn CreateColumn(PropertyInfo property);
    }
}
