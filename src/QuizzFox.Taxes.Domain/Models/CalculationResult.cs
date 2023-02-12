namespace QuizzFox.Taxes.Domain.Models;

public record DomainResult(bool Success = false, bool Error = false, string? Reason = default);

public sealed record DomainResult<TDetails>(bool Success = false, bool Error = false, string? Reason = default, TDetails? Details = default) :
    DomainResult(Success, Error, Reason);