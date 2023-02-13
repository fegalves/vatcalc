namespace QuizzFox.Taxes.Api.Responses;

public sealed record VatCalculusData(decimal GrossAmount, decimal NetAmount, decimal VatAmount);