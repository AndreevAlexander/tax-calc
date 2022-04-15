using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Cqrs.Contracts.Handler;

namespace TaxCalculator.Cqrs.Implementation.Bus;

public class CommandBus : ICommandBus
{
    private readonly List<IHandler> _handlers;

    public CommandBus(List<IHandler> handlers)
    {
        _handlers = handlers;
    }

    public Task<CommandResult> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _handlers.OfType<ICommandHandler<TCommand>>().FirstOrDefault();
        if (handler == null)
        {
            throw new Exception($"Can not retrieve handler for command {typeof(TCommand).FullName}");
        }

        return handler.HandleAsync(command);
    }
}