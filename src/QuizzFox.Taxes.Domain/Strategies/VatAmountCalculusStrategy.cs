using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Strategies;

internal sealed class VatAmountCalculusStrategy : StrategyBase
{
    public override VatCalculationTypes CalculationType => VatCalculationTypes.Vat;

    protected override DomainResult<VatCalculusDetails> CalculateVat(decimal multiplier, VatCalculationDetails details)
    {
        var vatAmount = details.VatAmount!.Value;
        var netAm = vatAmount / multiplier;

        return new DomainResult<VatCalculusDetails>(true, Details: new VatCalculusDetails(netAm + vatAmount, netAm, vatAmount));
    }
}