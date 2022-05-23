namespace TaxCalculator.WebFrontend.State.Contracts;

public interface INotifyStateChanged
{
    IStateManager StateManager { get;set; }
    
    void RaiseStateChanged<TState>(string eventName, TState? state);
}