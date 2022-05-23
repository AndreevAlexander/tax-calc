using Microsoft.AspNetCore.Components;
using TaxCalculator.WebFrontend.State;
using TaxCalculator.WebFrontend.State.Contracts;

namespace TaxCalculator.WebFrontend.Pages;

public class StateComponent : ComponentBase, INotifyStateChanged
{
    [Inject]
    public IStateManager StateManager { get; set; }
    
    public void RaiseStateChanged<TState>(string eventName, TState? state)
    {
        StateManager.SetState(eventName, state);
        StateHasChanged();
    }
}