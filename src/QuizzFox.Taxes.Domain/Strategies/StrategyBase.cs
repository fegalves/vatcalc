using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Strategies;

internal abstract class StrategyBase : ICalculusStrategy
{
    public abstract VatCalculationTypes CalculationType { get; }

    DomainResult<VatCalculusDetails> ICalculusStrategy.CalculateVat(VatCalculationDetails details) =>
        CalculateVat(details.VatRate / 100, details);

    protected abstract DomainResult<VatCalculusDetails> CalculateVat(decimal multiplier, VatCalculationDetails details);
}