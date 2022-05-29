using System;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Domain.ValueObjects;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class CalculateTaxesQuery : IQuery
{
    public Guid ProfileId { get; set; }

    public Guid? CurrencyId { get; set; } = null;

    public DateTime? From { get; set; }

    public DateTime? To { get; set; }
}