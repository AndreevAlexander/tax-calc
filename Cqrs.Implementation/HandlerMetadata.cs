using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Cqrs.Implementation;

public class HandlerMetadata : IHandlerMetadata
{
    public Type GenericTypeDefinition { get; set; }
    
    public IEnumerable<Type> GenericArguments { get; set; }
    
    public bool IsCommand { get; set; }
    
    public Type Type { get; set; }
}