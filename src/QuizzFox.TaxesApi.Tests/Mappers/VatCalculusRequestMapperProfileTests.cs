using AutoMapper;
using FluentAssertions;
using QuizzFox.Taxes.Api.Mappers;
using QuizzFox.Taxes.Api.Requests;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Api.Tests.Mappers;

public sealed class VatCalculusRequestMapperProfileTests
{
    [Fact]
    public void Assert_MappingApiCalculusRequestToDomainCalculationDetails_ShouldMapProperties()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            cfg.AddProfile<VatCalculusRequestMapperProfile>());

        IMapper mapper = new Mapper(mapperConfiguration);

        var request = new VatCalculusRequest(10, 10, 9, 1);
        var domain = mapper.Map<VatCalculationDetails>(request);

        request.Should().BeEquivalentTo(domain);
    }
}