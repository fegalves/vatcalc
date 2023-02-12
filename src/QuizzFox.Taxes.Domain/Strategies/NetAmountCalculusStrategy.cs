using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Strategies;

internal sealed class NetAmountCalculusStrategy : StrategyBase
{
    public override VatCalculationTypes CalculationType => VatCalculationTypes.Net;

    protected override DomainResult<VatCalculusDetails> CalculateVat(decimal multiplier, VatCalculationDetails details)
    {
        var net = details.NetAmount!.Value;
        var vatAm = net * multiplier;

        return new DomainResult<VatCalculusDetails>(true, Details: new VatCalculusDetails(net + vatAm, net, vatAm));
    }
}