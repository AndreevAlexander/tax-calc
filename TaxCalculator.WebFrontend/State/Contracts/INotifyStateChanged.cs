namespace TaxCalculator.WebFrontend.State.Contracts;

public interface INotifyStateChanged
{
    IStateManager StateManager { get;set; }
    
    void RiseStateChanged<TState>(string eventName, TState state);
}