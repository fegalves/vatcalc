using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Services;

internal sealed class TaxesCalculusService : ITaxesCalculusService
{
    private readonly ITaxesReferenceData _taxesReference;
    private readonly IDictionary<VatCalculationTypes, ICalculusStrategy> _availableStraegies;

    public TaxesCalculusService(ITaxesReferenceData taxesReference, IEnumerable<ICalculusStrategy> strategies)
    {
        _taxesReference = taxesReference;
        _availableStraegies = strategies.ToDictionary(s => s.CalculationType);
    }

    CalculationResult ITaxesCalculusService.CalculateVatDetails(string locale, VatCalculationDetails details)
    {
        var rates = _taxesReference.GetVatRates(locale);

        if (rates is null)
            return new CalculationResult(Reason: $"No rates found for given locale {locale}");

        if (!rates.Contains(details.VatRate))
            return new CalculationResult(Reason: $"The given rate {details.VatRate} was not found as a valid rate for locale {locale}");

        try
        {
            var calculationType = details.GetCalculationType();

            if (!_availableStraegies.TryGetValue(calculationType, out var strategy))
                return new CalculationResult(Reason: $"Could not find a proper calculation strategy for type {calculationType}");

            return strategy.CalculateVat(details);
        }
        catch (Exception exc)
        {
            return new CalculationResult(Reason: exc.Message);
        }
    }

    void ITaxesCalculusService.SaveVatRates(string locale, IEnumerable<decimal> vatRates) =>
        _taxesReference.SaveVatRates(locale, vatRates);
}