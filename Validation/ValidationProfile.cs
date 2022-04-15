namespace TaxCalculator.Validation;

public class ValidationProfile
{
    void ForModel<TModel>(Action<object> ruleBuilder)
    {
        
    }
}
/**
var profile = new ValidationProfile();

profile.ForModel<CreateTaxProfileCommand>((profileBuilder) => 
{
	profileBuilder.Property(nameof(CreateTaxProfileCommand.Name))
		.Required()
		.MinLength(10)
		.MaxLength(20)
		.Regex(string regex)
		.WithCustomRule<TCustomRule>();
});
*/