using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Interfaces;

public interface ITaxesReferenceData
{
    IEnumerable<decimal>? GetVatRates(string locale);

    void SaveVatRates(VatRegistrationDetails details);
}