using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Cqrs.Implementation;
using TaxCalculator.Cqrs.Implementation.Bus;
using TaxCalculator.Domain.Services.Identifier;
using TaxCalculator.Validation;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.WebFrontend;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Extensions;
using TaxCalculator.WebFrontend.Infrastructure;
using TaxCalculator.WebFrontend.Pages.AdditionalSpends.Validation;
using TaxCalculator.WebFrontend.Pages.Dashboard.Validation;
using TaxCalculator.WebFrontend.Pages.Incomes.Validation;
using TaxCalculator.WebFrontend.Pages.Taxes.Validation;
using TaxCalculator.WebFrontend.Pages.TaxProfiles.Validation;
using TaxCalculator.WebFrontend.State;
using TaxCalculator.WebFrontend.State.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("https://localhost:7001")});
builder.Services.AddScoped<WebApi>();

builder.Services.AddSingleton<IProfileProvider, ProfileProvider>(provider =>
{
    var profileProvider = new ProfileProvider();
    profileProvider.RegisterValidationProfile<TaxValidationProfile>();
    profileProvider.RegisterValidationProfile<IncomeValidationProfile>();
    profileProvider.RegisterValidationProfile<AdditionalSpendsValidationProfile>();
    profileProvider.RegisterValidationProfile<TaxProfileValidationProfile>();
    profileProvider.RegisterValidationProfile<DashboardValidationProfile>();

    return profileProvider;
});

builder.Services.AddScoped<IValidationEngine, ValidationEngine>(provider =>
{
    var profileProvider = provider.GetService<IProfileProvider>();
    var engine = new ValidationEngine(t => (IValidationRule) ActivatorUtilities.GetServiceOrCreateInstance(provider, t),
        profileProvider);
    return engine;
});

builder.Services.AddSingleton<IIdentifierService, IdentifierService>();
builder.Services.AddSingleton<ICache, Cache>();
builder.Services.AddSingleton<IHandlerLoader, HandlerLoader>();
builder.Services.AddScoped<IQueryBus, QueryBus>(provider => new QueryBus(provider.GetQueryHandler));
builder.Services.AddScoped<ICommandBus, CommandBus>(provider => new CommandBus(provider.GetCommandHandler));

builder.Services.AddSingleton<MappingBuilder>();
builder.Services.AddSingleton<IMapper, MapperDecorator>();

builder.Services.AddSingleton<IStateManager, StateManager>();

await builder.Build().RunAsync();