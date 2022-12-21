using DevExpress.Pdf.Native.BouncyCastle.Utilities;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.UI.Desktop.Views.TaxProfiles.Queries;
using TaxCalculator.UI.Desktop.Views.TaxProfilesManage;
using TaxCalculator.UI.MVVM;

namespace TaxCalculator.UI.Desktop.Views.TaxProfiles
{
    public class TaxProfileViewModel : BaseViewModel
    {
        private readonly IQueryBus _queryBus;
        private readonly INavigator _navigator;

        public AsyncWatcher<ObservableCollection<ModelContainer<TaxProfileModel>>> ItemsWatcher { get; set; }

        public ICommand AddNewCommand { get; set; }

        public TaxProfileViewModel(IQueryBus queryBus, INavigator navigator)
        {
            _queryBus = queryBus;
            _navigator = navigator;

            ItemsWatcher = new(LoadDataAsync());

            AddNewCommand = new AsyncCommand(AddNewCommandExecuteAsync);
        }

        public async Task AddNewCommandExecuteAsync()
        {
            _navigator.NavigateToFromView<TaxProfileManageView>(null);
        }

        private async Task<ObservableCollection<ModelContainer<TaxProfileModel>>> LoadDataAsync()
        {
            var taxProfiles = await _queryBus
                .ExecuteAsync<GetTaxProfilesQuery, List<TaxProfileModel>>(new GetTaxProfilesQuery());

            var containers = taxProfiles.Select(x => new ModelContainer<TaxProfileModel>(x));

            return new ObservableCollection<ModelContainer<TaxProfileModel>>(containers);
        }
    }
}
