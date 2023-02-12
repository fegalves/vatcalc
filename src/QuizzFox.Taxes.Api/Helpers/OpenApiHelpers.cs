using QuizzFox.Taxes.Api.Requests;

namespace QuizzFox.Taxes.Api.Helpers;

internal static class OpenApiHelpers
{
    public static RouteHandlerBuilder AddTaxesDescriptions(this RouteHandlerBuilder routeBuilder) => routeBuilder
        .WithName("Taxes")
        .WithDescription("Calculates product amounts based on country locale and valid VAT rates")
        .WithSummary("Provides a way to calculate amounts based on VAT rates")
        .Accepts<VatCalculusRequest>("application/json")
        .WithOpenApi(operation =>
        {
            operation.Parameters[0].Description = "Specifies a valid country locale. Used to identify VAT percentages (e.g.: de_AT).";
            operation.RequestBody.Description = "Specifies a request body for VAT calculus.";

            return operation;
        });
}