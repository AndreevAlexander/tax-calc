using TaxCalculator.Validation;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Validation;

public class TaxProfileValidationProfile : ValidationProfile
{
    public TaxProfileValidationProfile()
    {
        ForModel<CreateTaxProfileModel>(b =>
        {
            b.Property(p => p.Name)
                .Required();

            b.Property(p => p.Description)
                .Required();
        });
        
        ForModel<UpdateTaxProfileModel>(b =>
        {
            b.Property(p => p.Name)
                .Required();

            b.Property(p => p.Description)
                .Required();
        });
        
        ForModel<TaxProfileDropdown>(b =>
        {
            b.Property(p => p.Id)
                .WithCustomRule<SelectedTaxValidationRule>();
        });
    }
}