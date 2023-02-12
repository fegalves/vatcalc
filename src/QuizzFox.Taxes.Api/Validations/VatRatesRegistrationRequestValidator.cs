using FluentValidation;
using QuizzFox.Taxes.Api.Requests;

namespace QuizzFox.Taxes.Api.Validations;

internal sealed class VatRatesRegistrationRequestValidator : AbstractValidator<VatRatesRegistrationRequest>
{
	public VatRatesRegistrationRequestValidator()
	{
        RuleFor(r => r.VatRates)
            .Cascade(CascadeMode.Stop)
            .Must(rates => rates?.Any() ?? false).WithMessage("At least one VAT rate should be provided.")
            .Must(rates => rates.Any(r => r > 0m)).WithMessage("Zero or negative rates are not supported.");
        RuleFor(r => r.Locale).NotEmpty().Matches("[a-z]{2}_[A-Z]{2}");
    }
}