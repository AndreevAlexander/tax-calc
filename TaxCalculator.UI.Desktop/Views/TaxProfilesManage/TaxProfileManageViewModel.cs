using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Domain.Services.Identifier;
using TaxCalculator.UI.Desktop.Views.Taxes;
using TaxCalculator.UI.Desktop.Views.TaxProfiles;
using TaxCalculator.UI.Desktop.Views.TaxProfilesManage.Commands;
using TaxCalculator.UI.Desktop.Views.TaxProfilesManage.TaxConfiguration;
using TaxCalculator.UI.MVVM;
using ICommand = System.Windows.Input.ICommand;

namespace TaxCalculator.UI.Desktop.Views.TaxProfilesManage
{
    public class TaxProfileManageViewModel : BaseViewModel
    {
        private readonly ICommandBus _commandBus;
        
        private readonly IIdentifierService _identifierService;

        private readonly INavigator _navigator;

        public ModelContainer<TaxProfileModel> TaxProfileContainer { get; set; }

        public ObservableCollection<KeyValuePair<Guid, string>> Currencies { get; set; }

        public AsyncWatcher<ObservableCollection<TaxModel>> Taxes { get; set; }

        public ICommand SaveTaxProfileCommand { get; set; }

        public ICommand AddTaxCommand { get; set; }

        public TaxProfileManageViewModel(ICommandBus commandBus,
                                         IIdentifierService identifierService,
                                         INavigator navigator)
        {
            _commandBus = commandBus;
            _identifierService = identifierService;
            _navigator = navigator;

            TaxProfileContainer = new ModelContainer<TaxProfileModel>(new TaxProfileModel());

            SaveTaxProfileCommand = new AsyncCommand(SaveTaxProfileExecuteAsync);
            AddTaxCommand = new AsyncCommand(AddTaxExecuteAsync);

            Currencies = new ObservableCollection<KeyValuePair<Guid, string>>(_identifierService.Currencies.IdentifierValues);
            Taxes = new AsyncWatcher<ObservableCollection<TaxModel>>(LoadTaxConfigurationsAsync());
        }

        private async Task SaveTaxProfileExecuteAsync()
        {
            var result = await _commandBus.DispatchAsync(new SaveTaxProfileCommand
            {
                Model = TaxProfileContainer.ProjectChanges()
            });

            if (result.Status == CommandStatus.Success)
            {
                _navigator.NavigateTo<TaxProfileView>(null);
            }
        }

        private async Task<ObservableCollection<TaxModel>> LoadTaxConfigurationsAsync()
        {
            return new ObservableCollection<TaxModel>();
        }

        private async Task AddTaxExecuteAsync()
        {
            var dialog = new TaxConfigurationManagementView();

            dialog.XamlRoot = _navigator.GetCurrentView().Content.XamlRoot;

            await dialog.ShowAsync();
        }
    }
}
