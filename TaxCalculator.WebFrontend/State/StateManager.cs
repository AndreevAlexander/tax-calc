using TaxCalculator.WebFrontend.State.Contracts;

namespace TaxCalculator.WebFrontend.State;

public class StateManager : IStateManager
{
    public event EventHandler<StateChangedEventArgs>? OnStateChanged;
    
    private Dictionary<string, List<object>> _stateStorage;

    public StateManager()
    {
        _stateStorage = new();
    }
    
    public TState? GetActualState<TState>(string eventName)
    {
        TState? actualState = default;
        
        var hasState = _stateStorage.TryGetValue(eventName, out List<object>? states);
        if (hasState)
        {
            actualState = (TState?) states?.LastOrDefault();
        }

        return actualState;
    }

    public void SetState<TState>(string eventName, TState currentState)
    {
        var hasPreviousState = _stateStorage.TryGetValue(eventName, out List<object>? states);
        if (hasPreviousState)
        {
            states?.Add(currentState);
        }
        else
        {
            states = new();
            states.Add(currentState);
        }

        _stateStorage[eventName] = states;

        OnStateChanged?.Invoke(this, new StateChangedEventArgs
        {
            EventName = eventName,
            CurrentState = currentState
        });
    }
}