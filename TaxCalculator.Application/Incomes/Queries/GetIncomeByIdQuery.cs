using System;
using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.Incomes.Queries;

public class GetIncomeByIdQuery : IQuery
{
    public Guid IncomeId { get; set; }
}