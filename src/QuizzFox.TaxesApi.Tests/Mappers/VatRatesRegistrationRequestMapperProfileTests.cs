using AutoMapper;
using FluentAssertions;
using QuizzFox.Taxes.Api.Mappers;
using QuizzFox.Taxes.Api.Requests;
using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Api.Tests.Mappers;

public sealed class VatRatesRegistrationRequestMapperProfileTests
{
    [Fact]
    public void Assert_MappingApiRegistrationRequestToDomainRegistrationDetails_ShouldMapProperties()
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            cfg.AddProfile<VatRatesRegistrationRequestMapperProfile>());

        IMapper mapper = new Mapper(mapperConfiguration);

        var request = new VatRatesRegistrationRequest("de_AT", new[] { 10m });
        var domain = mapper.Map<VatRegistrationDetails>(request);

        request.Should().BeEquivalentTo(domain);
    }
}