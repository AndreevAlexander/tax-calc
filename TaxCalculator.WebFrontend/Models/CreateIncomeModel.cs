namespace TaxCalculator.WebFrontend.Models;

public class CreateIncomeModel
{
    public string Value { get; set; }

    public Guid TaxProfileId { get; set; }

    public /*DateTime*/string IncomeDate { get; set; }
}