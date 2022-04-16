namespace TaxCalculator.Validation;

public class RuleConfiguration
{
    public bool IsRequired { get; set; }
	
    public int? MaxLength { get; set; }
	
    public int? MinLength { get; set; }
	
    public string? Regex { get; set; }
	
    public List<Type> CustomValidators { get; }

    public RuleConfiguration()
    {
        CustomValidators = new List<Type>();
        MaxLength = null;
        MinLength = null;
        Regex = null;
    }
}
