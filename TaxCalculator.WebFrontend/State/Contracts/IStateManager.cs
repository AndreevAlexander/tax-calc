namespace TaxCalculator.WebFrontend.State.Contracts;

public interface IStateManager
{
    TState? GetActualState<TState>(string eventName);

    void SetState<TState>(string eventName, TState currentState);

    event EventHandler<StateChangedEventArgs> OnStateChanged;
}