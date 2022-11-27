using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxCalculator.Domain.Entities;
using TaxCalculator.UI.MVVM;

namespace TaxCalculator.UI.Desktop.Views.TaxProfiles
{
    public class TaxProfileViewModel : BaseViewModel
    {
        public List<ModelContainer> Items { get; set; }

        public ICommand RenameCommand { get; set; }

        public TaxProfileViewModel()
        {
            Items = new List<ModelContainer>
            {
                new (new TaxProfileModel
                {
                    Name = "test profile"
                })
            };

            RenameCommand = new AsyncCommand(RenameExecuteAsync);
        }

        public async Task RenameExecuteAsync()
        {
            await Task.Delay(2);
        }
    }
}
