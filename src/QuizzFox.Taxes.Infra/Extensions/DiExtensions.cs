using Microsoft.Extensions.DependencyInjection;
using QuizzFox.Taxes.Domain.Interfaces;

namespace QuizzFox.Taxes.Infra.Extensions;

public static class DiExtensions
{
    public static IServiceCollection AddTaxServiceReferenceData(this IServiceCollection services) => services
        .AddMemoryCache()
        .AddSingleton<ITaxesReferenceData, TaxesRepository>();
}