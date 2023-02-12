using Microsoft.Extensions.DependencyInjection;
using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Services;
using QuizzFox.Taxes.Domain.Strategies;

namespace QuizzFox.Taxes.Domain.Extensions;

public static class DiExtensions
{
    public static IServiceCollection AddTaxServiceDependencies(this IServiceCollection services) => services
        .AddSingleton<ITaxesCalculusService, TaxesCalculusService>()
        .AddSingleton<IEnumerable<ICalculusStrategy>>(_ => new ICalculusStrategy[]
        {
            new GrossAmountCalculusStrategy(),
            new NetAmountCalculusStrategy(),
            new VatAmountCalculusStrategy()
        });
}