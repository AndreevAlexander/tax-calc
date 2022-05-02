namespace TaxCalculator.Domain.Services.Identifier;

public class CurrencyIdentifier<T> : IIdentifier<T>
{
    public T Usd { get; set; }

    public T Eur { get; set; }

    public T Uah { get; set; }

    public T Pln { get; set; }
}
