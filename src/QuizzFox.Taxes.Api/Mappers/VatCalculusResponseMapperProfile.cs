using AutoMapper;
using QuizzFox.Taxes.Api.Responses;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Api.Mappers;

internal sealed class VatCalculusResponseMapperProfile : Profile
{
    public VatCalculusResponseMapperProfile()
    {
        CreateMap<VatCalculusDetails, VatCalculusData>()
            .ForPath(dest => dest.VatAmount, opt => opt.MapFrom(src => src.VatAmount))
            .ForPath(dest => dest.NetAmount, opt => opt.MapFrom(src => src.NetAmount))
            .ForPath(dest => dest.GrossAmount, opt => opt.MapFrom(src => src.GrossAmount));
        CreateMap<CalculationResult, VatCalculusReponse>()
            .ForPath(dest => dest.Success, opt => opt.MapFrom(src => src.Success))
            .ForPath(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason))
            .ForPath(dest => dest.Data, opt => opt.MapFrom(src => src.Details));
    }
}