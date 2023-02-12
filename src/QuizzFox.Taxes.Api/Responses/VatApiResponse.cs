namespace QuizzFox.Taxes.Api.Responses;

public record VatApiReponse(bool Success = false, bool Error = false, string? Reason = default);

public sealed record VatApiReponse<TData>(bool Success = false, bool Error = false, string? Reason = default, TData? Details = default) :
    VatApiReponse(Success, Error, Reason);