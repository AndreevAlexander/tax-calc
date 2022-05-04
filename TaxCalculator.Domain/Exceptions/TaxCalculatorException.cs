using System;

namespace TaxCalculator.Domain.Exceptions;

public class TaxCalculatorException : Exception
{
    public TaxCalculatorException(string message) : base(message)
    {
        
    }
}