// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Net.Http;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Cqrs.Implementation;
using TaxCalculator.Cqrs.Implementation.Bus;
using TaxCalculator.Domain.Services.Identifier;
using TaxCalculator.Persistence;
using TaxCalculator.UI.Data;
using TaxCalculator.UI.Data.Contracts;
using TaxCalculator.UI.Desktop.Controls.DataGrid;
using TaxCalculator.UI.Desktop.Views.Taxes;
using TaxCalculator.UI.Desktop.Views.TaxProfiles;
using TaxCalculator.UI.Desktop.Views.TaxProfilesManage;
using TaxCalculator.UI.Desktop.Views.TaxProfilesManage.TaxConfiguration;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TaxCalculator.UI.Desktop
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Microsoft.UI.Xaml.Application
    {
        public IHost AppHost { get; set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureViews(services);
            ConfigureViewModels(services);

            services.AddSingleton<ICache, Cache>();
            services.AddSingleton<IHandlerLoader, HandlerLoader>();
            services.AddSingleton<IIdentifierService, IdentifierService>();
            services.AddSingleton<IColumnFactory, ColumnFactory>();

            services.AddScoped<IQueryBus, QueryBus>(provider => new QueryBus(provider.GetQueryHandler));
            services.AddScoped<ICommandBus, CommandBus>(provider => new CommandBus(provider.GetCommandHandler));

            services.AddTransient<HttpClient>(provider => new HttpClient
                { BaseAddress = new Uri("https://localhost:7001/api/") });
            services.AddTransient<IWebApi, WebApi>();
        }

        private void ConfigureViews(IServiceCollection services)
        {
            //services.AddSingleton<Shell>();
            services.AddSingleton<INavigator, Shell>();

            services.AddTransient<TaxProfileView>();
        }

        private void ConfigureViewModels(IServiceCollection services)
        {
            services.AddTransient<TaxProfileViewModel>();
            services.AddTransient<TaxProfileManageViewModel>();
            services.AddTransient<TaxConfigurationManagementViewModel>();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            await AppHost.StartAsync();

            var shell = (Shell)AppHost.Services.GetRequiredService<INavigator>();

            shell.RegisterMenuItem<TaxProfileView>();
            shell.RegisterMenuItem<TaxView>();

            shell.Activate();
        }
    }
}
