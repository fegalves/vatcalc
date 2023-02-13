namespace QuizzFox.Taxes.Api.Requests;

internal sealed record VatCalculusRequest(decimal VatRate, decimal? GrossAmount, decimal? NetAmount, decimal? VatAmount);