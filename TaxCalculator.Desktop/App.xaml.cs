using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Cqrs.Implementation.Bus;
using TaxCalculator.Desktop.Helpers.ViewFactory;
using TaxCalculator.Desktop.Services;
using TaxCalculator.Desktop.Services.Contracts;
using TaxCalculator.Desktop.Views.Common.Layouts;
using TaxCalculator.Desktop.Views.TaxProfileManagement.TaxProfileGridView;

namespace TaxCalculator.Desktop
{
    public partial class App : System.Windows.Application
    {
        private readonly IHost _host;

        private readonly Frame _rootFrame;

        public App()
        {
            _host = new HostBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();

            _rootFrame = new Frame();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            ConfigureCqrs(services);
            
            services.AddSingleton<MainWindow>();
            services.AddSingleton<INavigationService, NavigationService>(provider => new NavigationService(_rootFrame, provider));
            
            services.RegisterView<TaxProfileGridView, TaxProfileGridViewModel>();
            services.RegisterView<Layout>();
        }

        private void ConfigureCqrs(IServiceCollection services)
        {
            services.AddScoped<IQueryBus, QueryBus>(provider => new QueryBus((queryType, resultType) => GetQueryHandler(provider, queryType, resultType)));
    
            services.AddScoped<ICommandBus, CommandBus>(provider => new CommandBus(commandType => GetCommandHandler(provider, commandType)));
        }

        public IHandler GetCommandHandler(IServiceProvider provider, Type commandType)
        {
            var loader = provider.GetService<IHandlerLoader>();
            return loader.LoadHandlersTypesForAssemblies(Assembly.GetAssembly(typeof(CreateTaxProfileCommand)))
                .Where(x => x.IsCommand && x.GenericArguments.Contains(commandType))
                .Select(x => (IHandler) ActivatorUtilities.GetServiceOrCreateInstance(provider, x.Type))
                .FirstOrDefault();
        }
    
        public IHandler GetQueryHandler(IServiceProvider provider, Type queryType, Type resultType)
        {
            var args = new[] {queryType, resultType};
            var loader = provider.GetService<IHandlerLoader>();
            return loader.LoadHandlersTypesForAssemblies(Assembly.GetAssembly(typeof(CreateTaxProfileCommand)))
                .Where(x => x.IsCommand == false && x.GenericArguments.All(arg => args.Contains(arg)))
                .Select(x => (IHandler) ActivatorUtilities.GetServiceOrCreateInstance(provider, x.Type))
                .FirstOrDefault();
        }
        
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            
            using var scope = _host.Services.CreateScope();

            var mainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();
            
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
        }
    }
}