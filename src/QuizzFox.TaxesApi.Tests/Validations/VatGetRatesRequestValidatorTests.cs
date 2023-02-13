using FluentAssertions;
using QuizzFox.Taxes.Api.Requests;
using QuizzFox.Taxes.Api.Validations;

namespace QuizzFox.Taxes.Api.Tests.Validations;

public sealed class VatGetRatesRequestValidatorTests
{
    [Theory]
    [InlineData("de", "'Locale' is not in the correct format.", false, true)]
    [InlineData(null, "'Locale' must not be empty.", false, true)]
    [InlineData("", "'Locale' is not in the correct format.", false, true)]
    [InlineData("d_", "'Locale' is not in the correct format.", false, true)]
    [InlineData("d_A", "'Locale' is not in the correct format.", false, true)]
    [InlineData("deAT", "'Locale' is not in the correct format.", false, true)]
    [InlineData("de-AT", "'Locale' is not in the correct format.", false, true)]
    [InlineData("DE_AT", "'Locale' is not in the correct format.", false, true)]
    [InlineData("DE_at", "'Locale' is not in the correct format.", false, true)]
    [InlineData("de_at", "'Locale' is not in the correct format.", false, true)]
    [InlineData("dde_ATT", "'Locale' is not in the correct format.", false, true)]
    [InlineData("de_AT", null, true, false)]
    [InlineData("pt_BR", null, true, false)]
    public void Assert_VatGetRatesRequest_ShouldValidate(string locale, string? errorMessage, bool isValid, bool hasErrorMessage)
    {
        var validator = new VatGetRatesRequestValidator();
        var request = new VatGetRatesRequest(locale);
        
        var result = validator.Validate(request);

        result.IsValid.Should().Be(isValid);

        if (hasErrorMessage)
            result.Errors.Any(e => e.ErrorMessage == errorMessage).Should().BeTrue();
    }
}