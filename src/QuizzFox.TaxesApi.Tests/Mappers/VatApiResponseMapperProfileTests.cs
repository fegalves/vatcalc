using AutoMapper;
using FluentAssertions;
using QuizzFox.Taxes.Api.Mappers;
using QuizzFox.Taxes.Api.Responses;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Api.Tests.Mappers;

public sealed class VatApiResponseMapperProfileTests
{
    [Fact]
    public void Assert_MappingGenericsDomainToGenericsApiContext_ShouldMapProperties()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            cfg.AddProfile<VatApiResponseMapperProfile>());

        IMapper mapper = new Mapper(mapperConfiguration);

        var domain = new DomainResult<int>(true, true, "Sample", 5);
        var response = mapper.Map<VatApiReponse<int>>(domain);

        domain.Should().BeEquivalentTo(response);
    }

    [Fact]
    public void Assert_MappingDomainToApiContext_ShouldMapProperties()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            cfg.AddProfile<VatApiResponseMapperProfile>());

        IMapper mapper = new Mapper(mapperConfiguration);

        var domain = new DomainResult(true, true, "Sample");
        var apiResponse = mapper.Map<VatApiReponse>(domain);

        domain.Should().BeEquivalentTo(apiResponse);
    }
}