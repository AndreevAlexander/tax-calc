namespace TaxCalculator.Cqrs.Contracts;

public interface IHandlerMetadata
{
    public Type GenericTypeDefinition { get; }
    
    public IEnumerable<Type> GenericArguments { get; }
    
    public bool IsCommand { get; }
    
    public Type Type { get; }
}