using AutoMapper;
using FluentAssertions;
using QuizzFox.Taxes.Api.Mappers;
using QuizzFox.Taxes.Api.Responses;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Api.Tests.Mappers;

public sealed class VatCalculusDataMapperProfileTests
{
    [Fact]
    public void Assert_MappingDomainCalculusToApiCalculus_ShouldMapProperties()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            cfg.AddProfile<VatCalculusDataMapperProfile>());

        IMapper mapper = new Mapper(mapperConfiguration);

        var domain = new VatCalculusDetails(10, 9, 1);
        var response = mapper.Map<VatCalculusData>(domain);

        domain.Should().BeEquivalentTo(response);
    }
}