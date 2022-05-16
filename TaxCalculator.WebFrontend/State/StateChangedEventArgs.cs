namespace TaxCalculator.WebFrontend.State;

public class StateChangedEventArgs
{
    public string EventName { get; set; }
    
    public object CurrentState { get; set; }
}