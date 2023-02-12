using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Strategies;

internal sealed class GrossAmountCalculusStrategy : StrategyBase
{
    public override VatCalculationTypes CalculationType => VatCalculationTypes.Gross;

    protected override CalculationResult CalculateVat(decimal multiplier, VatCalculationDetails details)
    {
        var gross = details.GrossAmount!.Value;
        var vatAmount = gross * multiplier;

        return new CalculationResult(true, Details: new VatCalculusDetails(gross, gross - vatAmount, vatAmount));
    }
}