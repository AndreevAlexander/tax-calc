using TaxCalculator.Validation;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.Taxes.Validation;

public class TaxValidationProfile : ValidationProfile
{
    public TaxValidationProfile()
    {
        ForModel<UpdateTaxModel>(b =>
        {
            b.Property(p => p.Amount)
                .Required()
                .IsNumeric();

            b.Property(p => p.TaxType)
                .Required();
        });
        
        ForModel<CreateTaxModel>(b =>
        {
            b.Property(p => p.Amount)
                .Required()
                .IsNumeric();

            b.Property(p => p.TaxType)
                .Required();
        });
    }
    
}