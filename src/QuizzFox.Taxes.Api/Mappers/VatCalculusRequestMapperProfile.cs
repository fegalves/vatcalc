using AutoMapper;
using QuizzFox.Taxes.Api.Requests;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Api.Mappers;

internal sealed class VatCalculusRequestMapperProfile : Profile
{
    public VatCalculusRequestMapperProfile()
    {
        CreateMap<VatCalculusRequest, VatCalculationDetails>()
            .ForPath(dest => dest.VatRate, opt => opt.MapFrom(src => src.VatRate))
            .ForPath(dest => dest.VatAmount, opt => opt.MapFrom(src => src.VatAmount))
            .ForPath(dest => dest.GrossAmount, opt => opt.MapFrom(src => src.GrossAmount))
            .ForPath(dest => dest.NetAmount, opt => opt.MapFrom(src => src.NetAmount));
    }
}