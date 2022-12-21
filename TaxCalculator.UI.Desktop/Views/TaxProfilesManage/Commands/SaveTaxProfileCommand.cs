using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.UI.Desktop.Views.TaxProfiles;

namespace TaxCalculator.UI.Desktop.Views.TaxProfilesManage.Commands
{
    public class SaveTaxProfileCommand : ICommand
    {
        public TaxProfileModel Model { get; set; }
    }
}
