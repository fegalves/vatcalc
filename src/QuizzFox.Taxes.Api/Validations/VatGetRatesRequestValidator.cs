using FluentValidation;
using QuizzFox.Taxes.Api.Requests;

namespace QuizzFox.Taxes.Api.Validations;

internal sealed class VatGetRatesRequestValidator : AbstractValidator<VatGetRatesRequest>
{
	public VatGetRatesRequestValidator() =>
        RuleFor(r => r.Locale).NotNull().Matches("^[a-z]{2}_[A-Z]{2}$");
}