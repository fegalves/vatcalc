using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Interfaces;

public interface ITaxesCalculusService
{
    DomainResult<VatCalculusDetails> CalculateVatDetails(string locale, VatCalculationDetails details);

    DomainResult SaveVatRates(VatRegistrationDetails details);

    DomainResult<IEnumerable<decimal>> GetRates(string locale);
}