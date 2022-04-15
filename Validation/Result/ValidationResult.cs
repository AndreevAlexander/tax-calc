namespace TaxCalculator.Validation.Result;

public class ValidationResult
{
    public ValidationState State { get; }

    public string? Message { get; }

    public ValidationResult(string? message, ValidationState state)
    {
        Message = message;
        State = state;
    }

    public static ValidationResult Valid()
    {
        return new ValidationResult(null, ValidationState.Valid);
    }
    
    public static ValidationResult Invalid(string message)
    {
        return new ValidationResult(message, ValidationState.Invalid);
    }
    
    public static ValidationResult Warning(string message)
    {
        return new ValidationResult(message, ValidationState.Warning);
    }
}