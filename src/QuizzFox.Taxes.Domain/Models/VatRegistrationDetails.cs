namespace QuizzFox.Taxes.Domain.Models;

public sealed record VatRegistrationDetails(string Locale, IEnumerable<decimal> VatRates);