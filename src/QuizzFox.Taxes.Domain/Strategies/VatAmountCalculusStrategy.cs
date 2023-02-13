using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Strategies;

internal sealed class VatAmountCalculusStrategy : ICalculusStrategy
{
    VatCalculationTypes ICalculusStrategy.CalculationType => VatCalculationTypes.Vat;

    DomainResult<VatCalculusDetails> ICalculusStrategy.CalculateVat(VatCalculationDetails details)
    {
        var multiplier = details.VatRate / 100;
        var vat = details.VatAmount!.Value;
        var net = vat / multiplier;

        return new DomainResult<VatCalculusDetails>(true, Details: new VatCalculusDetails(vat + net, net, vat));
    }
}