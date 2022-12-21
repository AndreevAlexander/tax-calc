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

        public bool RequiresIdentifierConversion { get; }

        public GeneratedColumnAttribute(string displayName, bool isReadOnly = false, bool requiresIdentifierConversion = false)
        {
            DisplayName = displayName;
            IsReadOnly = isReadOnly;
            RequiresIdentifierConversion = requiresIdentifierConversion;
        }
    }
}
