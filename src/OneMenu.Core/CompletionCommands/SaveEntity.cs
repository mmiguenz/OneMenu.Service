using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneMenu.Core.Constants;
using OneMenu.Core.Model;
using OneMenu.Core.Repositories;

namespace OneMenu.Core.CompletionCommands
{
    public class SaveEntity : ITransactionCompleteCommand
    {
        private readonly ISaveTransactionRepository _saveTransactionRepository;

        public SaveEntity(ISaveTransactionRepository saveTransactionRepository)
        {
            _saveTransactionRepository = saveTransactionRepository;
        }
        
        public string TransactionCompleteType => TransactionCompleteCommandType.SaveEntity;
       
        public async Task<string> CompleteTransaction(MenuTransaction menuTransaction)
        {
            var dictionary = new Dictionary<string, string>();
            try
            {
                foreach (var menuTransactionMenuStepResponse in menuTransaction.MenuStepResponses)
                {
                    var (fieldName, response) = (menuTransactionMenuStepResponse.Step.FieldName,
                        menuTransactionMenuStepResponse.Response);

                    if (!string.IsNullOrWhiteSpace(fieldName))
                        dictionary.Add(menuTransactionMenuStepResponse.Step.FieldName,
                            menuTransactionMenuStepResponse.Response);
                }

                await _saveTransactionRepository.SaveTransaction(dictionary);

                return "Transaccion exitosa";
            }
            catch (Exception _)
            {
                return "Error Procesando la transaccion";
            }
        }
    }
}