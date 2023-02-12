using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Interfaces;

public interface ITaxesCalculusService
{
    CalculationResult CalculateVatDetails(string locale, VatCalculationDetails details);

    void SaveVatRates(string locale, IEnumerable<decimal> vatRates);
}