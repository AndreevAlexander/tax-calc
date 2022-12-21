namespace TaxCalculator.Domain.Services.Identifier;

public class CurrencyIdentifier<T> : IdentifierBase<T>
{
    public T Usd { get; }

    public T Eur { get; }

    public T Uah { get; }

    public T Pln { get; }

    public CurrencyIdentifier(T usd, T eur, T uah, T pln)
    {
        Usd = usd;
        Eur = eur;
        Uah = uah;
        Pln = pln;

        InitializeIdentifierValues();
    }
}
