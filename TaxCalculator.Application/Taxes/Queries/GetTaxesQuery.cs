using System;
using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.Taxes.Queries;

public class GetTaxesQuery : IQuery
{
    public Guid? ProfileId { get; set; } = null;
    
    public int? Page { get; set; } = null;

    public int? PageSize { get; set; } = null;
}