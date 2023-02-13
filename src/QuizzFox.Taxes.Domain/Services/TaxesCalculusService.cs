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

    DomainResult<VatCalculusDetails> ITaxesCalculusService.CalculateVatDetails(string locale, VatCalculationDetails details)
    {
        try
        {
            var rates = _taxesReference.GetVatRates(locale);

            if (rates is null)
                return new DomainResult<VatCalculusDetails>(Reason: $"No rates found for given locale {locale}");

            if (!rates.Contains(details.VatRate))
                return new DomainResult<VatCalculusDetails>(Reason: $"The given rate {details.VatRate} was not found as a valid rate for locale {locale}");

            var calculationType = details.GetCalculationType();

            if (!_availableStraegies.TryGetValue(calculationType, out var strategy))
                return new DomainResult<VatCalculusDetails>(Reason: $"Could not find a proper calculation strategy for type {calculationType}");

            return strategy.CalculateVat(details);
        }
        catch (Exception exc)
        {
            return new DomainResult<VatCalculusDetails>(Error: true, Reason: exc.Message);
        }
    }

    DomainResult<IEnumerable<decimal>> ITaxesCalculusService.GetRates(string locale)
    {
        try
        {
            var rates = _taxesReference.GetVatRates(locale);

            if (rates is null)
                return new DomainResult<IEnumerable<decimal>>(Reason: $"No VAT rates found for locale {locale}");

            return new DomainResult<IEnumerable<decimal>>(true, Details: rates);
        }
        catch (Exception exc)
        {
            return new DomainResult<IEnumerable<decimal>>(Error: true, Reason: exc.Message);
        }
    }

    DomainResult ITaxesCalculusService.SaveVatRates(VatRegistrationDetails details)
    {
        try
        {
            _taxesReference.SaveVatRates(details);

            return new DomainResult(true);
        }
        catch(Exception exc)
        {
            return new DomainResult(Error: true, Reason: exc.Message);
        }
    }
}