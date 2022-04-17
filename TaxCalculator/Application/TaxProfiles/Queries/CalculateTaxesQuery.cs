using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class CalculateTaxesQuery : IQuery
{
    public DateTime? From { get; set; }

    public DateTime? To { get; set; }

    public Guid ProfileId { get; set; }
}