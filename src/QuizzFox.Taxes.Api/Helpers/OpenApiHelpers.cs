using QuizzFox.Taxes.Api.Requests;

namespace QuizzFox.Taxes.Api.Helpers;

internal static class OpenApiHelpers
{
    public static RouteHandlerBuilder AddTaxesAndCalculusDescriptions(this RouteHandlerBuilder routeBuilder) => routeBuilder
        .WithName("TaxesCalculus")
        .WithDescription("Calculates product amounts based on country locale and valid VAT rates.")
        .WithSummary("Provides a way to calculate product amounts based on VAT rates (for a given locale).")
        .WithOpenApi(operation =>
        {
            operation.Parameters[0].Description = "Specifies a valid country locale. Used to identify VAT percentages (e.g.: de_AT).";
            operation.RequestBody.Description = "Specifies a valid VAT rate (based on locale) and a valid amount as basis to calculate all product amounts.";
            
            return operation;
        })
        .Accepts<VatCalculusRequest>("application/json");

    public static RouteHandlerBuilder AddTaxRatesRegistrationDescriptions(this RouteHandlerBuilder routeBuilder) => routeBuilder
        .WithName("TaxRatesRegistration")
        .WithDescription("Creates (or updates) a given locale with one or more VAT rates.")
        .WithSummary("Saves VAT rates for a given locale.")
        .WithOpenApi(operation =>
        {
            operation.RequestBody.Description = "One or more rates for a given locale.";

            return operation;
        })
        .Accepts<VatRatesRegistrationRequest>("application/json");

    public static RouteHandlerBuilder AddGetTaxRatesDescriptions(this RouteHandlerBuilder routeBuilder) => routeBuilder
        .WithName("GetTaxRates")
        .WithDescription("Get VAT rates information based on a given locale.")
        .WithSummary("Returns a list of VAT rates for a given locale.")
        .WithOpenApi(operation =>
        {
            operation.Parameters[0].Description = "Specifies a valid country locale. Used to identify VAT percentages (e.g.: de_AT).";

            return operation;
        });
}