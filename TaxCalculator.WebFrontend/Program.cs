using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaxCalculator.Domain.Services.Identifier;
using TaxCalculator.Validation;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.WebFrontend;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Validation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("https://localhost:7001")});
builder.Services.AddScoped<WebApi>();
builder.Services.AddSingleton<IIdentifierService, IdentifierService>();
builder.Services.AddSingleton<IValidationEngine, ValidationEngine>(provider =>
{
    var engine = new ValidationEngine(t => (IValidationRule) ActivatorUtilities.GetServiceOrCreateInstance(provider, t));
    engine.RegisterValidationProfile<TaxValidationProfile>();
    engine.RegisterValidationProfile<IncomeValidationProfile>();

    return engine;
});

await builder.Build().RunAsync();