
namespace TaxCalculator.Domain.Services;

public interface ICurrencyConverterService<TModel> where TModel : class
{
    decimal ToSystem(decimal value, TModel model);
    decimal ToCurrency(decimal value, TModel model, Guid currencyId);
}