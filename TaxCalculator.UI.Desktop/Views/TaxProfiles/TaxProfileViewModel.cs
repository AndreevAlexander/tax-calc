using System.Threading.Tasks;
using System.Windows.Input;
using TaxCalculator.Domain.Entities;
using TaxCalculator.UI.MVVM;

namespace TaxCalculator.UI.Desktop.Views.TaxProfiles
{
    public class TaxProfileViewModel : BaseViewModel
    {
        public ModelContainer Model { get; set; }

        public ICommand RenameCommand { get; set; }

        public TaxProfileViewModel()
        {
            Model = new ModelContainer(new TaxProfile
            {
                Name = "test profile"
            });

            RenameCommand = new AsyncCommand(RenameExecuteAsync);
        }

        public async Task RenameExecuteAsync()
        {
            await Task.Delay(2);

            ((dynamic)Model).Name = "updated name";
        }
    }
}
