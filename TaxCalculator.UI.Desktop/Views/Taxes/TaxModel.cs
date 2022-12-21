using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Enums;

namespace TaxCalculator.UI.Desktop.Views.Taxes
{
    public class TaxModel
    {
        public double Amount { get; set; }

        public bool IsPercentage { get; set; }

        public decimal? AppliesBefore { get; set; }

        public Guid TaxProfileId { get; set; }

        public TaxType TaxType { get; set; }
    }
}
