namespace QuizzFox.Taxes.Api.Requests;

internal sealed record VatRatesRegistrationRequest(string Locale, IEnumerable<decimal> VatRates);