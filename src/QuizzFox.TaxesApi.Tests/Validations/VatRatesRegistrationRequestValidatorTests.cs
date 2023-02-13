using FluentAssertions;
using QuizzFox.Taxes.Api.Requests;
using QuizzFox.Taxes.Api.Validations;
using System.Collections;

namespace QuizzFox.Taxes.Api.Tests.Validations;

public sealed class VatRatesRegistrationRequestValidatorTests
{
    private sealed class VatRatesRegistrationRequestData : IEnumerable<object[]>
    {
        private readonly IEnumerable<object[]> _data = new[]
        {
            new object[] { "de_AT", null!, "At least one VAT rate should be provided.", false, true },
            new object[] { "de_AT", Array.Empty<decimal>(), "At least one VAT rate should be provided.", false, true },
            new object[] { "de_AT", new[] { 1m, 0m }, "Zero or negative rates are not supported.", false, true },
            new object[] { "de_AT", new[] { 1m, -1m }, "Zero or negative rates are not supported.", false, true },
            new object[] { "de", new[] { 1m }, "'Locale' is not in the correct format.", false, true },
            new object[] { null!, new[] { 1m }, "'Locale' must not be empty.", false, true },
            new object[] { "", new[] { 1m }, "'Locale' is not in the correct format.", false, true },
            new object[] { "d_", new[] { 1m }, "'Locale' is not in the correct format.", false, true },
            new object[] { "d_A", new[] { 1m }, "'Locale' is not in the correct format.", false, true },
            new object[] { "deAT", new[] { 1m }, "'Locale' is not in the correct format.", false, true },
            new object[] { "de-AT", new[] { 1m }, "'Locale' is not in the correct format.", false, true },
            new object[] { "DE_AT", new[] { 1m }, "'Locale' is not in the correct format.", false, true },
            new object[] { "DE_at", new[] { 1m }, "'Locale' is not in the correct format.", false, true },
            new object[] { "de_at", new[] { 1m }, "'Locale' is not in the correct format.", false, true },
            new object[] { "dde_ATT", new[] { 1m }, "'Locale' is not in the correct format.", false, true },
            new object[] { "de_AT", new[] { 1m }, null!, true, false },
            new object[] { "pt_BR", new[] { 1m }, null!, true, false }
        };

        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator() =>
            _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }

    [Theory]
    [ClassData(typeof(VatRatesRegistrationRequestData))]
    public void Assert_VatGetRegistrationRequest_ShouldValidate(string locale, IEnumerable<decimal> rates,
        string? errorMessage, bool isValid, bool hasErrorMessage)
    {
        var validator = new VatRatesRegistrationRequestValidator();
        var request = new VatRatesRegistrationRequest(locale, rates);
        
        var result = validator.Validate(request);

        result.IsValid.Should().Be(isValid);

        if (hasErrorMessage)
            result.Errors.Any(e => e.ErrorMessage == errorMessage).Should().BeTrue();
    }
}