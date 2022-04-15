namespace TaxCalculator.Cqrs.Contracts.Handler;

public interface ICommandHandler<TCommand> : IHandler where TCommand : ICommand
{
    Task<CommandResult> HandleAsync(TCommand command);
}