using TaxCalculator.Validation;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.Dashboard.Validation;

public class DashboardValidationProfile : ValidationProfile
{
    public DashboardValidationProfile()
    {
        ForModel<TaxProfileDropdownModel>(b =>
        {
            b.Property(p => p.TaxProfileId)
                .WithCustomRule<TaxProfileDropdownValidationRule>();

            b.Property(p => p.From)
                .WithCustomRule<DateFromValidationRule>();

            b.Property(p => p.To)
                .WithCustomRule<DateToValidationRule>();
        });
    }
}