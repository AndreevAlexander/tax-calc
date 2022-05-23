using TaxCalculator.Validation;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.Incomes.Validation;

public class IncomeValidationProfile : ValidationProfile
{
    public IncomeValidationProfile()
    {
        ForModel<IncomeModel>(builder =>
        {
            builder.Property(p => p.Value)
                .Required()
                .IsNumeric();

            builder.Property(p => p.IncomeDate)
                .Required();
        });
    }
}