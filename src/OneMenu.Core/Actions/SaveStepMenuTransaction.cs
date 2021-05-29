using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using OneMenu.Core.Model;
using OneMenu.Core.Repositories;

namespace OneMenu.Core.actions
{
    public class SaveStepMenuTransaction
    {
        private readonly IMenuTransactionRepository _menuTransactionRepository;
        private readonly IMenuRepository _menuRepository;

        public SaveStepMenuTransaction(IMenuTransactionRepository menuTransactionRepository,
            IMenuRepository menuRepository)
        {
            _menuTransactionRepository = menuTransactionRepository;
            _menuRepository = menuRepository;
        }

        public async Task<SaveStepResult> Execute(string transactionId, string response)
        {
            var menuTransaction = await _menuTransactionRepository.Get(transactionId);
            var menu = await _menuRepository.Get(menuTransaction.MenuId);
            var currentStep = menu.GetStepAt(menuTransaction.CurrentStepOrdinal);

            var stepResponse =  new MenuStepResponse(currentStep, response);

            menuTransaction.AddStepResponse(stepResponse);
            
            string completionMsg = "";
                
            if (menuTransaction.IsCompleted)
                completionMsg = ExecuteTransactionCommand();
            
            await _menuTransactionRepository.Save(menuTransaction);

            return new SaveStepResult()
            {
                CurrentStep = menuTransaction.IsCompleted ? null :  menu.GetStepAt(menuTransaction.CurrentStepOrdinal),
                CompletionMsg  = completionMsg,
                ValidationErrors = stepResponse.ValidationErrors
            };
        }

        private string ExecuteTransactionCommand()
        {
            return "Transaction Completed";
        }
    }
}