﻿using TaxCalculator.Validation;
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
        
        ForModel<TaxProfileDropdownModel>(b =>
        {
            b.Property(p => p.TaxProfileId)
                .WithCustomRule<TaxProfileDropdownValidationRule>();
        });
    }
}