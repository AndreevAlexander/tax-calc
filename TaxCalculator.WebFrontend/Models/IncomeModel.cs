namespace TaxCalculator.WebFrontend.Models;

public class IncomeModel
{
    public Guid Id { get; set; }
    
    public decimal? Value { get; set; }
    
    public Guid TaxProfileId { get; set; }

    public DateTime IncomeDate { get; set; } = DateTime.Now;
}