using FluentAssertions;
using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Models;
using QuizzFox.Taxes.Domain.Strategies;

namespace QuizzFox.Taxes.Domain.Tests.Strategies;

public sealed class NetAmountCalculusStrategyTests
{
    [Fact]
    public void Assert_GivenValidNetData_ShouldCalculateDetails()
    {
        ICalculusStrategy strategy = new NetAmountCalculusStrategy();

        var result = strategy.CalculateVat(new VatCalculationDetails(10m, null, 900m, null));

        strategy.CalculationType.Should().Be(VatCalculationTypes.Net);
        result.Error.Should().BeFalse();
        result.Reason.Should().BeNull();
        result.Success.Should().BeTrue();
        result.Details.Should().NotBeNull();
        result.Details!.NetAmount.Should().Be(900m);
        result.Details!.VatAmount.Should().Be(90m);
        result.Details!.GrossAmount.Should().Be(990m);
    }
}