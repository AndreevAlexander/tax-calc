using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.UI.Desktop.Controls.DataGrid
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GeneratedColumnAttribute : Attribute
    {
        public string DisplayName { get; }

        public bool IsReadOnly { get; }

        public GeneratedColumnAttribute(string displayName, bool isReadOnly = false)
        {
            DisplayName = displayName;
            IsReadOnly = isReadOnly;
        }
    }
}
