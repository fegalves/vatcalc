using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Strategies;

internal sealed class GrossAmountCalculusStrategy : ICalculusStrategy
{
    VatCalculationTypes ICalculusStrategy.CalculationType => VatCalculationTypes.Gross;

    DomainResult<VatCalculusDetails> ICalculusStrategy.CalculateVat(VatCalculationDetails details)
    {
        var multiplier = 100 / (details.VatRate + 100);
        var gross = Math.Round(details.GrossAmount!.Value, 2);
        var net = Math.Round(gross * multiplier, 2);

        return new DomainResult<VatCalculusDetails>(true, Details: new VatCalculusDetails(gross, net, gross - net));
    }
}