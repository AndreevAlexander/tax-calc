using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.UI.Data.Contracts
{
    public interface IWebApi
    {
        Task<TResult> GetOneAsync<TResult>(string route) where TResult : class;

        Task<IEnumerable<TResult>> GetManyAsync<TResult>(string route) where TResult : class;

        Task<Guid?> PostAsync(string route, object body);
        
        Task<bool> PutAsync(string route, object body);

        Task<bool> DeleteAsync(string route);
    }
}
