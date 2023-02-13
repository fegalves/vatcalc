using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Api;

internal sealed class LocaleFeeder : IHostedService
{
    private readonly ITaxesCalculusService _calculusService;

    public LocaleFeeder(ITaxesCalculusService calculusService) =>
        _calculusService = calculusService;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _calculusService.SaveVatRates(new VatRegistrationDetails("de_AT", new[] { 10m, 13m, 20m }));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}