using System.Threading.Tasks;

namespace TaxCalculator.Cqrs.Contracts.Bus;

public interface ICommandBus
{
    Task<CommandResult> DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
}