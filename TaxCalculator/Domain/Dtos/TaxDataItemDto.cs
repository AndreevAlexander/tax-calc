namespace TaxCalculator.Domain.Dtos;

public class TaxDataItemDto
{
    public string Title { get; set; }
    public decimal IncomeGross { get; set; }
    public decimal IncomeTax { get; set; }
    public decimal SocialTax { get; set; }
    public decimal IncomeNet { get; set; }
}