using TaxCalculator.Validation;
using TaxCalculator.WebFrontend.Models;
using TaxCalculator.WebFrontend.Pages.Dashboard.Validation;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Validation;

public class TaxProfileValidationProfile : ValidationProfile
{
    public TaxProfileValidationProfile()
    {
        ForModel<TaxProfileModel>(b =>
        {
            b.Property(p => p.Name)
                .Required();

            b.Property(p => p.Description)
                .Required();
        });
    }
}