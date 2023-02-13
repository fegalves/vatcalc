using AutoMapper;
using QuizzFox.Taxes.Api.Responses;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Api.Mappers;

internal sealed class VatApiResponseMapperProfile : Profile
{
    public VatApiResponseMapperProfile()
    {
        CreateMap(typeof(DomainResult), typeof(VatApiReponse));
        CreateMap(typeof(DomainResult<>), typeof(VatApiReponse<>));
    }
}