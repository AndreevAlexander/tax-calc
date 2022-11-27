using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ABI.System;
using TaxCalculator.Domain.Entities;
using TaxCalculator.UI.MVVM;

namespace TaxCalculator.UI.Desktop.Views.TaxProfiles
{
    public class TaxProfileViewModel : BaseViewModel
    {
        public ObservableCollection<ModelContainer<TaxProfileModel>> Items { get; set; }

        public ICommand AddNewCommand { get; set; }

        public TaxProfileViewModel()
        {
            Items = new ObservableCollection<ModelContainer<TaxProfileModel>>
            {
                new (new TaxProfileModel
                {
                    Name = "test profile"
                })
            };

            AddNewCommand = new AsyncCommand(AddNewCommandExecuteAsync);
        }

        public async Task AddNewCommandExecuteAsync()
        {
            await Task.Delay(2);

           // ((dynamic)Items[0]).Name = "asdasdasd";

            //await Task.Delay(1000);

            Items.Clear();

           // RaisePropertyChanged(nameof(Items));
        }
    }
}
