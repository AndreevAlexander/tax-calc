using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.UI.Data.Contracts;

namespace TaxCalculator.UI.Desktop.Views.TaxProfilesManage.Commands
{
    public class SaveTaxProfileCommandHandler : ICommandHandler<SaveTaxProfileCommand>
    {
        private readonly IWebApi _webApi;

        public SaveTaxProfileCommandHandler(IWebApi webApi)
        {
            _webApi = webApi;
        }

        public async Task<CommandResult> HandleAsync(SaveTaxProfileCommand command)
        {
            var model = command.Model;
            var commandResult = new CommandResult
            {
                RecordId = model.Id
            };

            try
            {
                if (model.Id.HasValue)
                {
                    var result = await _webApi.PutAsync("TaxProfile", model);
                    commandResult.Status = result ? CommandStatus.Success : CommandStatus.Fail;
                }
                else
                {
                    var result = await _webApi.PostAsync("TaxProfile", model);
                    commandResult.RecordId = result;
                    commandResult.Status = result.HasValue ? CommandStatus.Success : CommandStatus.Fail;
                }
            }
            catch
            {
                commandResult.Status = CommandStatus.Fail;
            }

            return commandResult;
        }
    }
}
