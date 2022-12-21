using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxCalculator.Domain.Enums;
using TaxCalculator.UI.Desktop.Views.Taxes;
using TaxCalculator.UI.MVVM;

namespace TaxCalculator.UI.Desktop.Views.TaxProfilesManage.TaxConfiguration
{
    public class TaxConfigurationManagementViewModel : BaseViewModel
    {
        public ModelContainer<TaxModel> TaxModel { get; set; }

        public ObservableCollection<KeyValuePair<int, string>> TaxTypes { get; set; }

        public ICommand SaveTaxConfigurationCommand { get; set; }

        public TaxConfigurationManagementViewModel()
        {
            TaxTypes = new ObservableCollection<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>((int)TaxType.IncomeTax, nameof(TaxType.IncomeTax)),
                new KeyValuePair<int, string>((int)TaxType.SocialTax, nameof(TaxType.SocialTax)),
            };

            TaxModel = new ModelContainer<TaxModel>(new TaxModel());

            SaveTaxConfigurationCommand = new AsyncCommand(SaveTaxConfigurationExecuteAsync);
        }

        private async Task SaveTaxConfigurationExecuteAsync()
        {

        }
    }
}
