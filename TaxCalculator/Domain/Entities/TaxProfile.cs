using TaxCalculator.Domain.Dtos;
using TaxCalculator.Domain.Enums;

namespace TaxCalculator.Domain.Entities;

public class TaxProfile : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }
    
    public ICollection<Tax> Taxes { get; set; }
    
    public ICollection<AdditionalSpend> AdditionalSpends { get; set; }
    
    public ICollection<Income> Incomes { get; set; }

    public TaxProfile()
    {
        Taxes = new HashSet<Tax>();
        AdditionalSpends = new HashSet<AdditionalSpend>();
        Incomes = new HashSet<Income>();
    }

    public IEnumerable<TaxDataItemDto> CalculateTaxes(DateTime? from = null, DateTime? to = null)
    {
        var incomes = Incomes.ToArray();
        if (from != null)
        {
            incomes = incomes.Where(x => x.CreatedDate.Month >= from.Value.Month 
                                         && x.CreatedDate.Year == from.Value.Year).ToArray();
        }

        if (to != null)
        {
            incomes = incomes.Where(x => x.CreatedDate.Month <= to.Value.Month 
                                         && x.CreatedDate.Year == to.Value.Year).ToArray();
        }
        
        var groupedIncomes = incomes.GroupBy(x => new
        {
            x.CreatedDate.Year,
            x.CreatedDate.Month
        });

        var totalIncome = Incomes.Sum(x => x.Value);
        var incomeTaxes = Taxes.Where(x => x.TaxType == TaxType.IncomeTax).ToArray();
        if (!incomeTaxes.Any())
        {
            throw new Exception("No income taxes were configured");
        }
        
        var incomeTax = incomeTaxes.Where(x => x.AppliesBefore != null && x.AppliesBefore < totalIncome)
            .OrderByDescending(x => x.AppliesBefore)
            .FirstOrDefault();
        
        if (incomeTax == null)
        {
            incomeTax = incomeTaxes.FirstOrDefault();
        }

        var socialTax = Taxes.FirstOrDefault(x => x.TaxType == TaxType.SocialTax);

        if (incomeTax == null)
        {
            incomeTax = incomeTaxes.FirstOrDefault();
        }

        var additionalSpends = AdditionalSpends.ToLookup(x => new {x.CreatedDate.Year, x.CreatedDate.Month});
        
        var result = new List<TaxDataItemDto>();
        foreach (var incomeGroup in groupedIncomes)
        {
            var incomePerMonth = incomeGroup.Sum(x => x.Value);

            var currentAdditionalSpends = additionalSpends[incomeGroup.Key];
            var beforeTaxAdditionalSpends = currentAdditionalSpends.Where(x => x.AppliedBeforeTax).Sum(x => x.Amount);

            incomePerMonth -= beforeTaxAdditionalSpends;

            var incomeTaxPerMonth = 0m;
            if (incomeTax.IsPercentage)
            {
                incomeTaxPerMonth = (incomePerMonth * (decimal) incomeTax.Amount) / 100;
            }
            else
            {
                incomeTaxPerMonth = (decimal)incomeTax.Amount;
            }

            var socialTaxPerMonth = 0m;
            if (socialTax != null)
            {
                if (socialTax.IsPercentage)
                {
                    socialTaxPerMonth = (incomePerMonth * (decimal) socialTax.Amount) / 100;
                }
                else
                {
                    socialTaxPerMonth = (decimal) socialTax.Amount;
                }
            }

            var incomePerMonthNet = incomePerMonth - (incomeTaxPerMonth + socialTaxPerMonth);
            
            var afterTaxAdditionalSpends = currentAdditionalSpends.Where(x => !x.AppliedBeforeTax).Sum(x => x.Amount);
            incomePerMonthNet -= afterTaxAdditionalSpends;

            result.Add(new TaxDataItemDto
            {
                Title = $"For: {incomeGroup.Key.Year}/{incomeGroup.Key.Month}",
                IncomeGross = incomePerMonth,
                IncomeTax = incomeTaxPerMonth,
                SocialTax = socialTaxPerMonth,
                IncomeNet = incomePerMonthNet
            });
        }

        return result;
    }
}