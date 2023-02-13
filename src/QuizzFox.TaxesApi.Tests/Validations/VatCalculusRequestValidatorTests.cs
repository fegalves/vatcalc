using FluentAssertions;
using QuizzFox.Taxes.Api.Requests;
using QuizzFox.Taxes.Api.Validations;
using System.Collections;
using Xunit.Sdk;

namespace QuizzFox.Taxes.Api.Tests.Validations;

public sealed class VatCalculusRequestValidatorTests
{
    private sealed class VatCalculusRequestInputData : IEnumerable<object[]>
    {
        private readonly IEnumerable<object[]> _data = new[]
        {
            new object[] { 0m, 10m, new decimal?()!, new decimal?()!, "'Vat Rate' must be greater than '0'.", false, true },
            new object[] { 10m, new decimal?()!, new decimal?()!, new decimal?()!, "'Vat Amount' must not be empty.", false, true },
            new object[] { 10m, new decimal?()!, new decimal?()!, new decimal?()!, "'Gross Amount' must not be empty.", false, true },
            new object[] { 10m, new decimal?()!, new decimal?()!, new decimal?()!, "'Net Amount' must not be empty.", false, true },
            new object[] { 10m, 0m, 0m, 0m, "'Vat Amount' must be greater than '0'.", false, true },
            new object[] { 10m, 0m, 0m, 0m, "'Gross Amount' must be greater than '0'.", false, true },
            new object[] { 10m, 0m, 0m, 0m, "'Net Amount' must be greater than '0'.", false, true },
            new object[] { 10m, 10m, 9m, 1m, "Only one of the fields (VatAmount, NetAmount or GrossAmount) should be filled per request", false, true },
            new object[] { 10m, 10m, 9m, new decimal?()!, "Only one of the fields (VatAmount, NetAmount or GrossAmount) should be filled per request", false, true },
            new object[] { 10m, 10m, new decimal?()!, 1m, "Only one of the fields (VatAmount, NetAmount or GrossAmount) should be filled per request", false, true },
            new object[] { 10m, new decimal?()!, 9m, 1m, "Only one of the fields (VatAmount, NetAmount or GrossAmount) should be filled per request", false, true },
            new object[] { 10m, 10m, new decimal?()!, new decimal?()!, null!, true, false },
            new object[] { 10m, new decimal?()!, 9m, new decimal?()!, null!, true, false },
            new object[] { 10m, new decimal?()!, new decimal?()!, 1m, null!, true, false }
        };

        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator() =>
            _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }

    [Theory]
    [ClassData(typeof(VatCalculusRequestInputData))]
    public void Assert_VatCalculusRequest_ShouldValidate(decimal vatRate, decimal? grossAmount, decimal? netAmount,
        decimal? vatAmount, string? errorMessage, bool isValid, bool hasErrorMessage)
    {
        var validator = new VatCalculusRequestValidator();
        var request = new VatCalculusRequest(vatRate, grossAmount, netAmount, vatAmount);
        
        var result = validator.Validate(request);

        result.IsValid.Should().Be(isValid);

        if (hasErrorMessage)
            result.Errors.Any(e => e.ErrorMessage == errorMessage).Should().BeTrue();
    }
}