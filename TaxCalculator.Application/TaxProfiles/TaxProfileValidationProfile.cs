﻿using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Validation;

namespace TaxCalculator.Application.TaxProfiles;

public class TaxProfileValidationProfile : ValidationProfile
{
    public TaxProfileValidationProfile()
    {
        ForModel<CreateTaxProfileCommand>(b =>
        {
            b.Property(x => x.Name)
                .Required();

            b.Property(x => x.Description)
                .Required();

            b.Property(x => x.ProfileCurrencyId)
                .Required();
        });
    }
}