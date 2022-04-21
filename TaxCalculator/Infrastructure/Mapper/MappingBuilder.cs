using AutoMapper;
using TaxCalculator.Application.Taxes.Commands;
using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Infrastructure.Mapper;

public class MappingBuilder
{
    public IMapper CreateMapper()
    {
        var mapper = new MapperConfiguration(x => x.CreateProfile("default", ConfigureMappings)).CreateMapper();
        return mapper;
    }

    private void ConfigureMappings(IProfileExpression profile)
    {
        profile.CreateMap<CreateTaxProfileCommand, TaxProfile>()
            .ForMember(x => x.ProfileCurrency, x => x.Ignore())
            .ForMember(x => x.ProfileCurrencyId, x => x.MapFrom(y => y.ProfileCurrencyId))
            .ForMember(x => x.Name, x => x.MapFrom(y => y.Name))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Description));
        
        profile.CreateMap<CreateTaxCommand, Tax>()
            .ForAllMembers(x => x.MapAtRuntime());
    }
}