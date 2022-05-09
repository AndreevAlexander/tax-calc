using TaxCalculator.Validation;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Validation;

public class IncomeValidationProfile : ValidationProfile
{
    public IncomeValidationProfile()
    {
        ForModel<CreateIncomeModel>(builder =>
        {
            builder.Property(p => p.Value)
                .Required()
                .IsNumeric();

            builder.Property(p => p.IncomeDate)
                .Regex("[0-9]{4}-[0-9]{2}-[0-9]{2}");
        });
        
        ForModel<UpdateIncomeModel>(builder =>
        {
            builder.Property(p => p.Value)
                .Required()
                .IsNumeric();

            builder.Property(p => p.IncomeDate)
                .Regex("[0-9]{4}-[0-9]{2}-[0-9]{2}");
        });
    }
}