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
            .ForAllMembers(x => x.MapAtRuntime());
        
        profile.CreateMap<CreateTaxCommand, Tax>()
            .ForAllMembers(x => x.MapAtRuntime());
    }
}