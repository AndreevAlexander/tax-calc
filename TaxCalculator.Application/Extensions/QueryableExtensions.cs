using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Application.Extensions;

public static class QueryableExtensions
{
    public static Task<List<TModel>> ToListAsync<TModel>(this IQueryable<TModel> queryable)
    {
        return Task.Factory.StartNew(queryable.ToList);
    }
}