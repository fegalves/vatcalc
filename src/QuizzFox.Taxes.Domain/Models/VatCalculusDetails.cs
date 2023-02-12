namespace QuizzFox.Taxes.Domain.Models;

public sealed record VatCalculusDetails(decimal GrossAmount, decimal NetAmount, decimal VatAmount);