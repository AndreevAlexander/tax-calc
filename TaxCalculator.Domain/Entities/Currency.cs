using System.Collections.Generic;

namespace TaxCalculator.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }

        public double ExchangeRate { get; set; }

        public ICollection<TaxProfile> TaxProfiles { get; set; }
    }
}