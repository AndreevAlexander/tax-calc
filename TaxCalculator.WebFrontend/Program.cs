using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaxCalculator.Domain.Services.Identifier;
using TaxCalculator.WebFrontend;
using TaxCalculator.WebFrontend.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("https://localhost:7001")});
builder.Services.AddScoped<WebApi>();
builder.Services.AddSingleton<IIdentifierService, IdentifierService>();

await builder.Build().RunAsync();