namespace QuizzFox.Taxes.Domain.Models;

public sealed record CalculationResult(bool Success = false, string? Reason = default, VatCalculusDetails? Details = default);