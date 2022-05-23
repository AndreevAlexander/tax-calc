using TaxCalculator.Validation;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Validation;

public class AdditionalSpendsValidationProfile : ValidationProfile
{
    public AdditionalSpendsValidationProfile()
    {
        ForModel<AdditionalSpendModel>(b =>
        {
            b.Property(p => p.Amount)
                .Required()
                .IsNumeric();
        });
    }
}