using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Strategies;

internal sealed class NetAmountCalculusStrategy : StrategyBase
{
    public override VatCalculationTypes CalculationType => VatCalculationTypes.Net;

    protected override CalculationResult CalculateVat(decimal multiplier, VatCalculationDetails details)
    {
        var net = details.NetAmount!.Value;
        var vatAm = net * multiplier;

        return new CalculationResult(true, Details: new VatCalculusDetails(net + vatAm, net, vatAm));
    }
}