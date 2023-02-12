using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using QuizzFox.Taxes.Api;
using QuizzFox.Taxes.Api.Helpers;
using QuizzFox.Taxes.Api.Requests;
using QuizzFox.Taxes.Api.Responses;
using QuizzFox.Taxes.Api.Validations;
using QuizzFox.Taxes.Domain.Extensions;
using QuizzFox.Taxes.Domain.Interfaces;
using QuizzFox.Taxes.Domain.Models;
using QuizzFox.Taxes.Infra.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddScoped<IValidator<VatCalculusRequest>, VatCalculusRequestValidator>()
    .AddTaxServiceDependencies()
    .AddTaxServiceReferenceData()
    .AddAutoMapper(Assembly.GetExecutingAssembly())
    .AddHostedService<LocaleFeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/taxes/{locale}",
    [ProducesResponseTypeAttribute(typeof(VatCalculusReponse), StatusCodes.Status200OK)]
    [ProducesResponseTypeAttribute(typeof(VatCalculusReponse), StatusCodes.Status400BadRequest)]
(
        IValidator<VatCalculusRequest> validator,
        IMapper mapper,
        [FromServices] ITaxesCalculusService calculusService,
        string locale,
        VatCalculusRequest request) =>
{
    var result = validator.Validate(request);

    if (!result.IsValid)
        return Results.ValidationProblem(result.ToDictionary());

    var details = mapper.Map<VatCalculationDetails>(request);

    var calculusResult = calculusService.CalculateVatDetails(locale, details);

    var response = mapper.Map<VatCalculusReponse>(calculusResult);

    return response.Success ?
        Results.Ok(response) :
        Results.BadRequest(response);
}).AddTaxesDescriptions();

app.Run();