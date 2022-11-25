using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.UI.Desktop.Attributes
{
    public class NavigationDetailsAttribute : Attribute
    {
        public string DisplayName { get; }
        
        public string Glyph { get; }

        public string Font { get; }

        public NavigationDetailsAttribute(string displayName, string glyph, string font = "Segoe MDL2 Assets")
        {
            DisplayName = displayName;
            Glyph = glyph;
            Font = font;
        }
    }
}
