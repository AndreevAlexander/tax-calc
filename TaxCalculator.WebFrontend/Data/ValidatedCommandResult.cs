using TaxCalculator.Validation.Result;

namespace TaxCalculator.WebFrontend.Data;

public class ValidatedCommandResult
{
    public string Status { get; set; } = string.Empty;
    
    public Guid? RecordId { get; set; }

    public Dictionary<string, IEnumerable<ValidationResult>> Warnings { get; set; } = new();
}