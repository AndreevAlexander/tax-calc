using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using TaxCalculator.Domain.Services.Identifier;

namespace TaxCalculator.UI.Desktop.Converters
{
    public class IdentifierConverter : IValueConverter
    {
        public IIdentifierService IdentifierService { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            object result = null;
            if (value != null)
            {
                var identifierId = (Guid)value;
                result = IdentifierService.Currencies[identifierId];
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return Guid.Empty;
            }

            var kvp = (KeyValuePair<Guid, string>)value;
            return kvp.Key;
        }
    }
}
