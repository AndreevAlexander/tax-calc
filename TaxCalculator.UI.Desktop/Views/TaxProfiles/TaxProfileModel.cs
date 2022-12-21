using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Entities;
using TaxCalculator.UI.Desktop.Controls.DataGrid;
using TaxCalculator.UI.Desktop.Converters;

namespace TaxCalculator.UI.Desktop.Views.TaxProfiles
{
    public class TaxProfileModel
    {
        public Guid? Id { get; set; }

        [GeneratedColumn("Name")]
        public string Name { get; set; }

        [GeneratedColumn("Description")]
        public string Description { get; set; }

        [GeneratedColumn("Currency", requiresIdentifierConversion: true)]
        public Guid? ProfileCurrencyId { get; set; }

        /*public ICollection<Tax> Taxes { get; set; }

        public ICollection<AdditionalSpend> AdditionalSpends { get; set; }

        public ICollection<Income> Incomes { get; set; }

        public Currency ProfileCurrency { get; set; }

        public Guid? ProfileCurrencyId { get; set; }*/
    }
}
