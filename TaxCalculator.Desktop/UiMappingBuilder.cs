using AutoMapper;
using TaxCalculator.Application.Taxes.Commands;
using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Infrastructure.Mapper;

namespace TaxCalculator.Desktop;

public class UiMappingBuilder : MappingBuilder, IMappingBuilder
{
    public new IMapper CreateMapper()
    {
        var mapper = new MapperConfiguration(x => x.CreateProfile("default", ConfigureMappings)).CreateMapper();
        return mapper;
    }

    protected override void ConfigureMappings(IProfileExpression profile)
    {
        base.ConfigureMappings(profile);
        
        profile.CreateMap<TaxProfile, TaxProfileModel>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Description, o => o.MapFrom(x => x.Description))
            .ForMember(x => x.Currency, o => o.MapFrom(x => x.ProfileCurrency));

        profile.CreateMap<Currency, CurrencyModel>()
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.ExchangeRate, o => o.MapFrom(x => x.ExchangeRate))
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ReverseMap();

        profile.CreateMap<Tax, TaxModel>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount))
            .ForMember(x => x.IsPercentage, o => o.MapFrom(x => x.IsPercentage))
            .ForMember(x => x.AppliesBefore, o => o.MapFrom(x => x.AppliesBefore))
            .ForMember(x => x.TaxType, o => o.MapFrom(x => x.TaxType.ToString()))
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.TaxProfileId));

        profile.CreateMap<TaxModel, CreateTaxCommand>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount))
            .ForMember(x => x.AppliesBefore, o => o.MapFrom(x => x.AppliesBefore))
            .ForMember(x => x.IsPercentage, o => o.MapFrom(x => x.IsPercentage))
            .ForMember(x => x.TaxType, o => o.MapFrom(x => x.TaxType))
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.TaxProfileId));
        
        profile.CreateMap<TaxModel, UpdateTaxCommand>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount))
            .ForMember(x => x.AppliesBefore, o => o.MapFrom(x => x.AppliesBefore))
            .ForMember(x => x.IsPercentage, o => o.MapFrom(x => x.IsPercentage))
            .ForMember(x => x.TaxType, o => o.MapFrom(x => x.TaxType));

        profile.CreateMap<TaxProfileModel, UpdateTaxProfileCommand>()
            .ForMember(x => x.Description, o => o.MapFrom(x => x.Description))
            .ForMember(x => x.TaxProfileId, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name));
    }
}