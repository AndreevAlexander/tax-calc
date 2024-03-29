﻿using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.Incomes.Commands;

public class CreateIncomeCommand : ICommand
{
    public decimal Value { get; set; }

    public Guid TaxProfileId { get; set; }

    public DateTime IncomeDate { get; set; }
}