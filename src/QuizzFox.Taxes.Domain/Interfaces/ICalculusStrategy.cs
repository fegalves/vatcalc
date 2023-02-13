using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Interfaces;

internal interface ICalculusStrategy
{
    public VatCalculationTypes CalculationType { get; }

    DomainResult<VatCalculusDetails> CalculateVat(VatCalculationDetails details);
}