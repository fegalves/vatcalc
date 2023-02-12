using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
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
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddScoped<IValidator<VatCalculusRequest>, VatCalculusRequestValidator>()
    .AddScoped<IValidator<VatRatesRegistrationRequest>, VatRatesRegistrationRequestValidator>()
    .AddTaxServiceDependencies()
    .AddTaxServiceReferenceData()
    .AddAutoMapper(Assembly.GetExecutingAssembly())
    .AddHostedService<LocaleFeeder>();

var app = builder.Build();

var culture = CultureInfo.GetCultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    SupportedCultures = new List<CultureInfo> { culture },
    SupportedUICultures = new List<CultureInfo> { culture },
    DefaultRequestCulture = new RequestCulture(culture)
};

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/taxes/{locale}/calculate",
    [ProducesResponseTypeAttribute(typeof(VatApiReponse<VatCalculusData>), StatusCodes.Status200OK)]
    [ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest)]
    [ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError)]
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

    var response = mapper.Map<VatApiReponse<VatCalculusData>>(calculusResult);

    if (response.Success)
        return Results.Ok(response);

    if (response.Error)
        return Results.Problem(CreateProblemDetails(
                StatusCodes.Status500InternalServerError, new { exception = response.Reason }));

    return response.Details is null ?
        Results.Problem(CreateProblemDetails(StatusCodes.Status404NotFound, new { reason = response.Reason })) :
        Results.Problem(CreateProblemDetails(StatusCodes.Status400BadRequest, new { reason = response.Reason }));
}).AddTaxesAndCalculusDescriptions();

app.MapPost("/taxes",
    [ProducesResponseTypeAttribute(StatusCodes.Status201Created)]
    [ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest)]
    [ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError)]
(
        IValidator<VatRatesRegistrationRequest> validator,
        IMapper mapper,
        [FromServices] ITaxesCalculusService calculusService,
        VatRatesRegistrationRequest request) =>
    {
        var result = validator.Validate(request);

        if (!result.IsValid)
            return Results.ValidationProblem(result.ToDictionary());

        var details = mapper.Map<VatRegistrationDetails>(request);

        var updateResult = calculusService.SaveVatRates(details);

        var response = mapper.Map<VatApiReponse>(updateResult);

        return response.Success ?
            Results.CreatedAtRoute(routeName: "GetTaxRates", routeValues: new { details.Locale }) :
            Results.Problem(CreateProblemDetails(
                StatusCodes.Status500InternalServerError, new { exception = response.Reason }));
    }).AddTaxRatesRegistrationDescriptions();

app.MapGet("/taxes/{locale}",
    [ProducesResponseTypeAttribute(typeof(VatApiReponse<IEnumerable<decimal>>), StatusCodes.Status200OK)]
    [ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest)]
    [ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError)]
(
        IMapper mapper,
        [FromServices] ITaxesCalculusService calculusService,
        [FromRoute] string locale) =>
    {
        var request = new VatGetRatesRequest(locale);
        var result = new VatGetRatesRequestValidator().Validate(request);

        if (!result.IsValid)
            return Results.ValidationProblem(result.ToDictionary());

        var getResult = calculusService.GetRates(request.Locale);

        var response = mapper.Map<VatApiReponse<IEnumerable<decimal>>>(getResult);

        if (response.Success)
            return Results.Ok(response);

        if (response.Error)
            return Results.Problem(CreateProblemDetails(
                StatusCodes.Status500InternalServerError, new { exception = response.Reason }));

        return !response.Details!.Any() ?
            Results.Problem(CreateProblemDetails(
                StatusCodes.Status404NotFound, new { reason = response.Reason, locale = request.Locale })) :
            Results.Problem(CreateProblemDetails(
                StatusCodes.Status400BadRequest, new { reason = response.Reason }));
    }).AddGetTaxRatesDescriptions();

app.Run();

static ProblemDetails CreateProblemDetails(int statusCode, object error)
{
    return new ProblemDetails
    {
        Title = "Unable to process this request",
        Status = statusCode,
        Extensions =
        {
            { "errors", error }
        }
    };
}