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
        profile.CreateMap<CreateAdditionalSpendModel, CreateAdditionalSpendCommand>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount.ToDecimal()))
            .ForMember(x => x.AppliedBeforeTax, o => o.MapFrom(x => x.AppliedBeforeTax))
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.TaxProfileId.ToGuid()));
        
        profile.CreateMap<AdditionalSpendModel, UpdateAdditionalSpendModel>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount.ToString()))
            .ForMember(x => x.AppliedBeforeTax, o => o.MapFrom(x => x.AppliedBeforeTax))
            .ForMember(x => x.AdditionalSpendId, o => o.MapFrom(x => x.Id.ToString()));

        profile.CreateMap<UpdateAdditionalSpendModel, UpdateAdditionalSpendCommand>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount.ToDecimal()))
            .ForMember(x => x.AppliedBeforeTax, o => o.MapFrom(x => x.AppliedBeforeTax))
            .ForMember(x => x.AdditionalSpendId, o => o.MapFrom(x => x.AdditionalSpendId.ToGuid()));
    }

    private void SetupIncomeMapping(IProfileExpression profile)
    {
        profile.CreateMap<CreateIncomeModel, CreateIncomeCommand>()
            .ForMember(x => x.Value, o => o.MapFrom(x => x.Value.ToDecimal()))
            .ForMember(x => x.IncomeDate, o => o.MapFrom(x => DateTime.Parse(x.IncomeDate)))
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.TaxProfileId));

        profile.CreateMap<IncomeModel, UpdateIncomeModel>()
            .ForMember(x => x.Value, o => o.MapFrom(x => x.Value.ToString()))
            .ForMember(x => x.IncomeDate, o => o.MapFrom(x => x.IncomeDate.ToString("yyyy-MM-dd")))
            .ForMember(x => x.IncomeId, o => o.MapFrom(x => x.Id));

        profile.CreateMap<UpdateIncomeModel, UpdateIncomeCommand>()
            .ForMember(x => x.Value, o => o.MapFrom(x => x.Value.ToDecimal()))
            .ForMember(x => x.IncomeDate, o => o.MapFrom(x => DateTime.Parse(x.IncomeDate)))
            .ForMember(x => x.IncomeId, o => o.MapFrom(x => x.IncomeId.ToGuid()));
    }

    private void SetupTaxMapping(IProfileExpression profile)
    {
        profile.CreateMap<CreateTaxModel, CreateTaxCommand>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount.ToDouble()))
            .ForMember(x => x.AppliesBefore, o => o.MapFrom(x => x.AppliesBefore.ToDecimal()))
            .ForMember(x => x.IsPercentage, o => o.MapFrom(x => x.IsPercentage))
            .ForMember(x => x.TaxType, o => o.MapFrom(x => Enum.Parse<TaxType>(x.TaxType)))
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.TaxProfileId));

        profile.CreateMap<TaxModel, UpdateTaxModel>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount.ToString()))
            .ForMember(x => x.AppliesBefore, o => o.MapFrom(x => x.AppliesBefore.ToString()))
            .ForMember(x => x.IsPercentage, o => o.MapFrom(x => x.IsPercentage))
            .ForMember(x => x.TaxType, o => o.MapFrom(x => x.TaxType.ToString()));
        
        profile.CreateMap<UpdateTaxModel, UpdateTaxCommand>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount.ToDouble()))
            .ForMember(x => x.AppliesBefore, o => o.MapFrom(x => x.AppliesBefore.ToDecimal()))
            .ForMember(x => x.IsPercentage, o => o.MapFrom(x => x.IsPercentage))
            .ForMember(x => x.TaxType, o => o.MapFrom(x => Enum.Parse<TaxType>(x.TaxType)));
    }

    private void SetupTaxProfileMapping(IProfileExpression profile)
    {
        profile.CreateMap<CreateTaxProfileModel, CreateTaxProfileCommand>()
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.Description, o => o.MapFrom(x => x.Description))
            .ForMember(x => x.ProfileCurrencyId, o => o.MapFrom(x => x.ProfileCurrencyId.ToGuid()));

        profile.CreateMap<TaxProfileModel, UpdateTaxProfileModel>()
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.Id.ToString()))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.Description, o => o.MapFrom(x => x.Description));
        
        profile.CreateMap<UpdateTaxProfileModel, UpdateTaxProfileCommand>()
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.TaxProfileId.ToGuid()))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.Description, o => o.MapFrom(x => x.Description));
    }
}