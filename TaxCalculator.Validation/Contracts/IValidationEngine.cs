using System.Threading.Tasks;

namespace TaxCalculator.Validation.Contracts
{
    public interface IValidationEngine
    {
        Task<ValidationResultContainer> ValidateAsync<TModel>(TModel model, object context = null) where TModel : class;
    }
}