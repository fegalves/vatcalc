using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Strategies;

internal sealed class NetAmountCalculusStrategy : ICalculusStrategy
{
    VatCalculationTypes ICalculusStrategy.CalculationType => VatCalculationTypes.Net;

    DomainResult<VatCalculusDetails> ICalculusStrategy.CalculateVat(VatCalculationDetails details)
    {
        var multiplier = details.VatRate / 100;
        var net = details.NetAmount!.Value;
        var vat = net * multiplier;

        return new DomainResult<VatCalculusDetails>(true, Details: new VatCalculusDetails(vat + net, net, vat));
    }
}