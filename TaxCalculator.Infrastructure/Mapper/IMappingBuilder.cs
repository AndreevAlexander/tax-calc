using AutoMapper;

namespace TaxCalculator.Infrastructure.Mapper;

public interface IMappingBuilder
{
    IMapper CreateMapper();
}