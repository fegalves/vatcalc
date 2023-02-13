using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Models;
using QuizzFox.Taxes.Domain.Services;

namespace QuizzFox.Taxes.Domain.Tests.Services;

public sealed class TaxesCalculusServiceTests
{
    [Fact]
    public void Assert_GivenNoRatesFoundForLocale_ShouldFail()
    {
        var taxesReference = Substitute.For<ITaxesReferenceData>();
        ITaxesCalculusService calculusService = new TaxesCalculusService(taxesReference, new ICalculusStrategy[0]);

        taxesReference.GetVatRates(Arg.Any<string>()).Returns((IEnumerable<decimal>)null!);

        var result = calculusService.CalculateVatDetails("pt_BR", new VatCalculationDetails(10m, 1000m, null, null));

        result.Success.Should().BeFalse();
        result.Error.Should().BeFalse();
        result.Reason.Should().Be("No rates found for given locale pt_BR");
    }

    [Fact]
    public void Assert_GivenProvidedRateNotFoundForLocale_ShouldFail()
    {
        var taxesReference = Substitute.For<ITaxesReferenceData>();
        ITaxesCalculusService calculusService = new TaxesCalculusService(taxesReference, new ICalculusStrategy[0]);

        var result = calculusService.CalculateVatDetails("pt_BR", new VatCalculationDetails(10m, 1000m, null, null));

        result.Success.Should().BeFalse();
        result.Error.Should().BeFalse();
        result.Reason.Should().Be("The given rate 10 was not found as a valid rate for locale pt_BR");
    }


    [Fact]
    public void Assert_GivenCalculationStrategyNotFound_ShouldFail()
    {
        var taxesReference = Substitute.For<ITaxesReferenceData>();
        ITaxesCalculusService calculusService = new TaxesCalculusService(taxesReference, new ICalculusStrategy[0]);

        taxesReference.GetVatRates(Arg.Any<string>()).Returns(new[] { 10m });

        var result = calculusService.CalculateVatDetails("pt_BR", new VatCalculationDetails(10m, 1000m, null, null));

        result.Success.Should().BeFalse();
        result.Error.Should().BeFalse();
        result.Reason.Should().Be($"Could not find a proper calculation strategy for type {VatCalculationTypes.Gross}");
    }

    [Fact]
    public void Assert_GivenAnExceptionHappens_ShouldNotThrow()
    {
        var taxesReference = Substitute.For<ITaxesReferenceData>();
        ITaxesCalculusService calculusService = new TaxesCalculusService(taxesReference, new ICalculusStrategy[0]);

        taxesReference.GetVatRates(Arg.Any<string>()).Throws(new Exception("That was unexpected"));

        var result = calculusService.CalculateVatDetails("pt_BR", new VatCalculationDetails(10m, 1000m, null, null));

        result.Success.Should().BeFalse();
        result.Error.Should().BeTrue();
        result.Reason.Should().Be("That was unexpected");
    }

    [Fact]
    public void Assert_IfEverythingGoesFine_ShouldPlayNicely()
    {
        var taxesReference = Substitute.For<ITaxesReferenceData>();
        var strategy = Substitute.For<ICalculusStrategy>();

        strategy.CalculationType.Returns(VatCalculationTypes.Gross);

        ITaxesCalculusService calculusService = new TaxesCalculusService(taxesReference,
            new ICalculusStrategy[]
            {
                strategy
            });

        strategy.CalculateVat(Arg.Any<VatCalculationDetails>()).Returns(new DomainResult<VatCalculusDetails>(true));
        taxesReference.GetVatRates(Arg.Any<string>()).Returns(new[] { 10m });

        var result = calculusService.CalculateVatDetails("pt_BR", new VatCalculationDetails(10m, 1000m, null, null));

        result.Success.Should().BeTrue();
        result.Error.Should().BeFalse();
        result.Reason.Should().BeNull();
    }
}