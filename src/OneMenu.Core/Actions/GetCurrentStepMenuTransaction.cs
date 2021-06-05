using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using OneMenu.Core.Model;
using OneMenu.Core.Model.Menus;
using OneMenu.Core.Repositories;

namespace OneMenu.Core.actions
{
    public class GetCurrentStepMenuTransaction
    {
        private readonly IMenuTransactionRepository _menuTransactionRepository;
        private readonly IMenuRepository _menuRepository;

        public GetCurrentStepMenuTransaction(IMenuTransactionRepository menuTransactionRepository,
            IMenuRepository menuRepository)
        {
            _menuTransactionRepository = menuTransactionRepository;
            _menuRepository = menuRepository;
        }

        public async Task<Step> Execute(string transactionId)
        {
            var menuTransaction = await _menuTransactionRepository.Get(transactionId);
            
            if (menuTransaction == null)
            {
                return null;
            }
            
            var menu = await _menuRepository.Get(menuTransaction.MenuId);

            return menu.GetStepAt(menuTransaction.CurrentStepOrdinal);
        }
    }
}