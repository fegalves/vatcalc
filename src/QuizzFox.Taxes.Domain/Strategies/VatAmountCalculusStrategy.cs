using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Strategies;

internal sealed class VatAmountCalculusStrategy : StrategyBase
{
    public override VatCalculationTypes CalculationType => VatCalculationTypes.Vat;

    protected override CalculationResult CalculateVat(decimal multiplier, VatCalculationDetails details)
    {
        var vatAmount = details.VatAmount!.Value;
        var netAm = vatAmount / multiplier;

        return new CalculationResult(true, Details: new VatCalculusDetails(netAm + vatAmount, netAm, vatAmount));
    }
}