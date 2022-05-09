using TaxCalculator.Validation;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Validation;

public class AdditionalSpendsValidationProfile : ValidationProfile
{
    public AdditionalSpendsValidationProfile()
    {
        ForModel<CreateAdditionalSpendModel>(b =>
        {
            b.Property(p => p.Amount)
                .Required()
                .IsNumeric();
        });
    }
}