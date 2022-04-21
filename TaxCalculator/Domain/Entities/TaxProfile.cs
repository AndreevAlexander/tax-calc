using TaxCalculator.Domain.Dtos;
using TaxCalculator.Domain.Enums;
using TaxCalculator.Domain.Exceptions;

namespace TaxCalculator.Domain.Entities;

public class TaxProfile : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }
    
    public ICollection<Tax> Taxes { get; set; }
    
    public ICollection<AdditionalSpend> AdditionalSpends { get; set; }
    
    public ICollection<Income> Incomes { get; set; }

    public Currency? ProfileCurrency { get; set; }
    
    public Guid? ProfileCurrencyId { get; set; }

    public TaxProfile()
    {
        Taxes = new HashSet<Tax>();
        AdditionalSpends = new HashSet<AdditionalSpend>();
        Incomes = new HashSet<Income>();
    }

    public IEnumerable<TaxDataItemDto> CalculateTaxes()
    {
        var incomes = Incomes.ToArray();
        
        var groupedIncomes = incomes.GroupBy(x => new
        {
            x.CreatedDate.Year,
            x.CreatedDate.Month
        });

        var totalIncome = incomes.Sum(x => x.Value);

        var incomeTax = GetIncomeTax(totalIncome);
        var socialTax = Taxes.FirstOrDefault(x => x.TaxType == TaxType.SocialTax);

        var additionalSpends = AdditionalSpends.ToLookup(x => new {x.CreatedDate.Year, x.CreatedDate.Month});
        
        var result = new List<TaxDataItemDto>();
        foreach (var incomeGroup in groupedIncomes)
        {
            var incomePerMonth = incomeGroup.Sum(x => x.Value);

            var currentAdditionalSpends = additionalSpends[incomeGroup.Key].ToArray();
            var beforeTaxAdditionalSpends = currentAdditionalSpends.Where(x => x.AppliedBeforeTax).Sum(x => x.Amount);

            incomePerMonth -= beforeTaxAdditionalSpends;

            var incomeTaxPerMonth = CalculateIncomeTax(incomeTax, incomePerMonth);
            var socialTaxPerMonth = CalculateSocialTax(socialTax, incomePerMonth);
            
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

    private Tax GetIncomeTax(decimal totalIncome)
    {
        var incomeTaxes = Taxes.Where(x => x.TaxType == TaxType.IncomeTax).ToArray();
        if (!incomeTaxes.Any())
        {
            throw new TaxCalculatorException("No income taxes were configured");
        }
        
        var incomeTax = incomeTaxes.Where(x => x.AppliesBefore != null && x.AppliesBefore < totalIncome)
            .OrderByDescending(x => x.AppliesBefore)
            .FirstOrDefault();
        
        if (incomeTax == null)
        {
            incomeTax = incomeTaxes.FirstOrDefault();
        }

        return incomeTax;
    }

    private decimal CalculateIncomeTax(Tax incomeTax, decimal incomePerMonth)
    {
        var incomeTaxPerMonth = 0m;
        if (incomeTax.IsPercentage)
        {
            incomeTaxPerMonth = (incomePerMonth * (decimal) incomeTax.Amount) / 100;
        }
        else
        {
            incomeTaxPerMonth = (decimal)incomeTax.Amount;
        }

        return incomeTaxPerMonth;
    }

    private decimal CalculateSocialTax(Tax? socialTax, decimal incomePerMonth)
    {
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
        
        return socialTaxPerMonth;
    }
}