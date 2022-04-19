using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Exceptions;

namespace TaxCalculator.Cqrs.Implementation.Bus;

public class CommandBus : ICommandBus
{
    private readonly CommandHandlerResolver _handlerResolver;

    public CommandBus(CommandHandlerResolver handlerResolver)
    {
        _handlerResolver = handlerResolver;
    }

    public Task<CommandResult> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = (ICommandHandler<TCommand>)_handlerResolver(typeof(TCommand));
        if (handler == null)
        {
            throw new TaxCalculatorException($"Can not retrieve handler for command {typeof(TCommand).FullName}");
        }

        return handler.HandleAsync(command);
    }
}