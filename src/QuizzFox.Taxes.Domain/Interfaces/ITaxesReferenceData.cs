namespace QuizzFox.Taxes.Domain.Interfaces;

public interface ITaxesReferenceData
{
    IEnumerable<decimal>? GetVatRates(string locale);

    void SaveVatRates(string locale, IEnumerable<decimal> vatRates);
}