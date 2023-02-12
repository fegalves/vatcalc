using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Strategies;

internal sealed class GrossAmountCalculusStrategy : StrategyBase
{
    public override VatCalculationTypes CalculationType => VatCalculationTypes.Gross;

    protected override DomainResult<VatCalculusDetails> CalculateVat(decimal multiplier, VatCalculationDetails details)
    {
        var gross = details.GrossAmount!.Value;
        var vatAmount = gross * multiplier;

        return new DomainResult<VatCalculusDetails>(true, Details: new VatCalculusDetails(gross, gross - vatAmount, vatAmount));
    }
}