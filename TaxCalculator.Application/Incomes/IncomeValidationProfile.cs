using TaxCalculator.Application.Incomes.Commands;
using TaxCalculator.Validation;

namespace TaxCalculator.Application.Incomes;

public class IncomeValidationProfile : ValidationProfile
{
    public IncomeValidationProfile()
    {
        ForModel<AddIncomeCommand>(x =>
        {
            x.Property(p => p.IncomeDate)
                .Required();
        });
    }
}