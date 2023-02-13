using System.Diagnostics;

namespace QuizzFox.Taxes.Domain.Models;

internal enum VatCalculationTypes
{
    Vat = 0,
    Gross = 1,
    Net = 2
}

public sealed record VatCalculationDetails(decimal VatRate, decimal? GrossAmount, decimal? NetAmount, decimal? VatAmount)
{
    internal VatCalculationTypes GetCalculationType() =>
        this switch
        {
            { GrossAmount: > 0, } => VatCalculationTypes.Gross,
            { NetAmount: > 0 } => VatCalculationTypes.Net,
            { VatAmount: > 0 } => VatCalculationTypes.Vat,
            _ => throw new UnreachableException("At least one amount should be assigned to proceed.")
        };
}