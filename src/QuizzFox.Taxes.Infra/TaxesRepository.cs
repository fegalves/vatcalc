using Microsoft.Extensions.Caching.Memory;
using QuizzFox.Taxes.Domain.Interfaces;

namespace QuizzFox.Taxes.Infra;

internal sealed class TaxesRepository : ITaxesReferenceData
{
    private readonly IMemoryCache _memoryCache;

    public TaxesRepository(IMemoryCache memoryCache) =>
        _memoryCache = memoryCache;

    IEnumerable<decimal>? ITaxesReferenceData.GetVatRates(string locale) =>
        (IEnumerable<decimal>?)_memoryCache.Get(locale);

    void ITaxesReferenceData.SaveVatRates(string locale, IEnumerable<decimal> vatRates) =>
        _memoryCache.Set(locale, vatRates);
}