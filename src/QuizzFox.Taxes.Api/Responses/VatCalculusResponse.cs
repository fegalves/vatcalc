namespace QuizzFox.Taxes.Api.Responses;

public sealed record VatCalculusReponse(bool Success = false, string? Reason = default, VatCalculusData? Data = default);

public sealed record VatCalculusData(decimal GrossAmount, decimal NetAmount, decimal VatAmount);