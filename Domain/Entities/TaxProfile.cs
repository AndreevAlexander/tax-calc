namespace TaxCalculator.Domain.Entities;

public class TaxProfile : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }
    
    public ICollection<Tax> Taxes { get; set; }
    
    public ICollection<AdditionalSpend> AdditionalSpends { get; set; }

    public TaxProfile()
    {
        Taxes = new HashSet<Tax>();
        AdditionalSpends = new HashSet<AdditionalSpend>();
    }
}