using AutoMapper;
using QuizzFox.Taxes.Api.Responses;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Api.Mappers;

internal sealed class VatCalculusDataMapperProfile : Profile
{
    public VatCalculusDataMapperProfile()
    {
        CreateMap<VatCalculusDetails, VatCalculusData>()
            .ForPath(dest => dest.VatAmount, opt => opt.MapFrom(src => src.VatAmount))
            .ForPath(dest => dest.NetAmount, opt => opt.MapFrom(src => src.NetAmount))
            .ForPath(dest => dest.GrossAmount, opt => opt.MapFrom(src => src.GrossAmount));
    }
}