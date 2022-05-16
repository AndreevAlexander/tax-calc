namespace TaxCalculator.WebFrontend.Extensions;

public static class StringExtensions
{
    public static Guid ToGuid(this string s)
    {
        return Guid.Parse(s);
    }

    public static decimal? ToDecimal(this string? s)
    {
        var parsed = decimal.TryParse(s, out decimal result);
        return parsed ? result : null;
    }

    public static double? ToDouble(this string? s)
    {
        var parsed = double.TryParse(s, out double result);
        return parsed ? result : null;
    }
}