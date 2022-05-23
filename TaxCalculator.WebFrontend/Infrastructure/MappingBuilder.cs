using AutoMapper;
using TaxCalculator.Domain.Enums;
using TaxCalculator.WebFrontend.Extensions;
using TaxCalculator.WebFrontend.Models;
using TaxCalculator.WebFrontend.Pages.AdditionalSpends.Commands;
using TaxCalculator.WebFrontend.Pages.Incomes.Commands;
using TaxCalculator.WebFrontend.Pages.Taxes.Commands;
using TaxCalculator.WebFrontend.Pages.TaxProfiles.Commands;

namespace TaxCalculator.WebFrontend.Infrastructure;

public class MappingBuilder
{
    public IMapper CreateMapper()
    {
        var mapper = new MapperConfiguration(x => x.CreateProfile("default", ConfigureMappings)).CreateMapper();
        return mapper;
    }

    private void ConfigureMappings(IProfileExpression profile)
    {
        SetupAdditionalSpendMapping(profile);
        SetupIncomeMapping(profile);
        SetupTaxMapping(profile);
        SetupTaxProfileMapping(profile);
    }

    private void SetupAdditionalSpendMapping(IProfileExpression profile)
    {
        profile.CreateMap<AdditionalSpendModel, CreateAdditionalSpendCommand>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount))
            .ForMember(x => x.AppliedBeforeTax, o => o.MapFrom(x => x.AppliedBeforeTax))
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.TaxProfileId));

        profile.CreateMap<AdditionalSpendModel, UpdateAdditionalSpendCommand>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount))
            .ForMember(x => x.AppliedBeforeTax, o => o.MapFrom(x => x.AppliedBeforeTax))
            .ForMember(x => x.AdditionalSpendId, o => o.MapFrom(x => x.Id));
    }

    private void SetupIncomeMapping(IProfileExpression profile)
    {
        profile.CreateMap<IncomeModel, CreateIncomeCommand>()
            .ForMember(x => x.Value, o => o.MapFrom(x => x.Value))
            .ForMember(x => x.IncomeDate, o => o.MapFrom(x => x.IncomeDate))
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.TaxProfileId));

        profile.CreateMap<IncomeModel, UpdateIncomeCommand>()
            .ForMember(x => x.Value, o => o.MapFrom(x => x.Value))
            .ForMember(x => x.IncomeDate, o => o.MapFrom(x => x.IncomeDate))
            .ForMember(x => x.IncomeId, o => o.MapFrom(x => x.Id));
    }

    private void SetupTaxMapping(IProfileExpression profile)
    {
        profile.CreateMap<TaxModel, CreateTaxCommand>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount))
            .ForMember(x => x.AppliesBefore, o => o.MapFrom(x => x.AppliesBefore))
            .ForMember(x => x.IsPercentage, o => o.MapFrom(x => x.IsPercentage))
            .ForMember(x => x.TaxType, o => o.MapFrom(x => x.TaxType))
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.TaxProfileId));

        profile.CreateMap<TaxModel, UpdateTaxCommand>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount))
            .ForMember(x => x.AppliesBefore, o => o.MapFrom(x => x.AppliesBefore))
            .ForMember(x => x.IsPercentage, o => o.MapFrom(x => x.IsPercentage))
            .ForMember(x => x.TaxType, o => o.MapFrom(x => x.TaxType));
    }

    private void SetupTaxProfileMapping(IProfileExpression profile)
    {
        profile.CreateMap<TaxProfileModel, CreateTaxProfileCommand>()
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.Description, o => o.MapFrom(x => x.Description))
            .ForMember(x => x.ProfileCurrencyId, o => o.MapFrom(x => x.ProfileCurrencyId));

        profile.CreateMap<TaxProfileModel, UpdateTaxProfileCommand>()
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.Description, o => o.MapFrom(x => x.Description));
    }
}