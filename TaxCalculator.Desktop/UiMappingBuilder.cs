﻿using AutoMapper;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Infrastructure.Mapper;

namespace TaxCalculator.Desktop;

public class UiMappingBuilder : IMappingBuilder
{
    public IMapper CreateMapper()
    {
        var mapper = new MapperConfiguration(x => x.CreateProfile("default", ConfigureMappings)).CreateMapper();
        return mapper;
    }

    private void ConfigureMappings(IProfileExpression profile)
    {
        profile.CreateMap<TaxProfile, TaxProfileModel>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Description, o => o.MapFrom(x => x.Description))
            .ForMember(x => x.Currency, o => o.MapFrom(x => x.ProfileCurrency));

        profile.CreateMap<Currency, CurrencyModel>()
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.ExchangeRate, o => o.MapFrom(x => x.ExchangeRate))
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id));

        profile.CreateMap<Tax, TaxModel>()
            .ForMember(x => x.Amount, o => o.MapFrom(x => x.Amount))
            .ForMember(x => x.IsPercentage, o => o.MapFrom(x => x.IsPercentage))
            .ForMember(x => x.AppliesBefore, o => o.MapFrom(x => x.AppliesBefore))
            .ForMember(x => x.TaxType, o => o.MapFrom(x => x.TaxType.ToString()))
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id));
    }
}