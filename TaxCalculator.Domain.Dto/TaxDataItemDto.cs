namespace TaxCalculator.Domain.Dto
{
    public class TaxDataItemDto
    {
        public string Month { get; set; }

        public decimal IncomeGross { get; set; }

        public decimal IncomeTax { get; set; }

        public decimal SocialTax { get; set; }

        public decimal IncomeNet { get; set; }
    }
}