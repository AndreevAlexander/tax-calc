using TaxCalculator.Contracts;

namespace TaxCalculator.Infrastructure.Mapper;

public class MapperDecorator : IMapper
{
    private readonly AutoMapper.IMapper _mapper;
    
    public MapperDecorator(IMappingBuilder builder)
    {
        _mapper = builder.CreateMapper();
    }

    public TDestination Map<TDestination>(object source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TSource, TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper.Map(source, destination);
    }
}