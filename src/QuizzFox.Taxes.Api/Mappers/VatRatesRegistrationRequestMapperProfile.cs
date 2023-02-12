using AutoMapper;
using QuizzFox.Taxes.Api.Requests;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Api.Mappers;

internal sealed class VatRatesRegistrationRequestMapperProfile : Profile
{
    public VatRatesRegistrationRequestMapperProfile()
    {
        CreateMap<VatRatesRegistrationRequest, VatRegistrationDetails>()
            .ForPath(dest => dest.Locale, opt => opt.MapFrom(src => src.Locale))
            .ForPath(dest => dest.VatRates, opt => opt.MapFrom(src => src.VatRates));
    }
}