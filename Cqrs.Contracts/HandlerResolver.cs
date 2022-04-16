using TaxCalculator.Cqrs.Contracts.Handler;

namespace TaxCalculator.Cqrs.Contracts;

public delegate object CommandHandlerResolver(Type commandType);

public delegate object QueryHandlerResolver(Type queryType, Type resultType);