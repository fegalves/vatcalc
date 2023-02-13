using FluentValidation;
using QuizzFox.Taxes.Api.Requests;

namespace QuizzFox.Taxes.Api.Validations;

internal sealed class VatCalculusRequestValidator : AbstractValidator<VatCalculusRequest>
{
	public VatCalculusRequestValidator()
	{
        var exclusiveFieldRequired = "Only one of the fields (VatAmount, NetAmount or GrossAmount) should be filled per request";

        RuleFor(r => r.VatRate).GreaterThan(0);
		RuleFor(r => r.VatAmount).NotEmpty().GreaterThan(0).Unless(r => r.NetAmount > 0 || r.GrossAmount > 0);
        RuleFor(r => r.VatAmount).Must(v => v.GetValueOrDefault() == 0).When(r => r.NetAmount.HasValue || r.GrossAmount.HasValue).WithMessage(exclusiveFieldRequired);
        RuleFor(r => r.GrossAmount).NotEmpty().GreaterThan(0).Unless(r => r.NetAmount > 0 || r.VatAmount > 0);
        RuleFor(r => r.GrossAmount).Must(v => v.GetValueOrDefault() == 0).When(r => r.NetAmount.HasValue || r.VatAmount.HasValue).WithMessage(exclusiveFieldRequired);
        RuleFor(r => r.NetAmount).NotEmpty().GreaterThan(0).Unless(r => r.GrossAmount > 0 || r.VatAmount > 0);
        RuleFor(r => r.NetAmount).Must(v => v.GetValueOrDefault() == 0).When(r => r.GrossAmount.HasValue || r.VatAmount.HasValue).WithMessage(exclusiveFieldRequired);
    }
}