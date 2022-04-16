using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Validation;

namespace TaxCalculator.Application.TaxProfiles;

public class TaxProfileValidationProfile : ValidationProfile
{
    public TaxProfileValidationProfile()
    {
        ForModel<CreateTaxProfileCommand>(b =>
        {
            b.Property(nameof(CreateTaxProfileCommand.Name))
                .MinLength(5);

            b.Property(nameof(CreateTaxProfileCommand.Description))
                .Required();
        });
    }
}