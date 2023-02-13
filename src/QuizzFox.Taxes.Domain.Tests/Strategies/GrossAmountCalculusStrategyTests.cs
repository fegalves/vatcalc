using FluentAssertions;
using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Models;
using QuizzFox.Taxes.Domain.Strategies;

namespace QuizzFox.Taxes.Domain.Tests.Strategies;

public sealed class GrossAmountCalculusStrategyTests
{
    [Fact]
    public void Assert_GivenValidGrossData_ShouldCalculateDetails()
    {
        ICalculusStrategy strategy = new GrossAmountCalculusStrategy();

        var result = strategy.CalculateVat(new VatCalculationDetails(10m, 1000m, null, null));

        strategy.CalculationType.Should().Be(VatCalculationTypes.Gross);
        result.Error.Should().BeFalse();
        result.Reason.Should().BeNull();
        result.Success.Should().BeTrue();
        result.Details.Should().NotBeNull();
        result.Details!.NetAmount.Should().Be(909.09m);
        result.Details!.VatAmount.Should().Be(90.91m);
        result.Details!.GrossAmount.Should().Be(1000m);
    }
}