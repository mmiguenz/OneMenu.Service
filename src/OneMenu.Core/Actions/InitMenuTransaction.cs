using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using OneMenu.Core.Model;
using OneMenu.Core.Repositories;

namespace OneMenu.Core.actions
{
    public class InitMenuTransaction
    {
        private readonly IMenuTransactionRepository _menuTransactionRepository;
        private readonly IMenuRepository _menuRepository;

        public InitMenuTransaction(IMenuTransactionRepository menuTransactionRepository,
            IMenuRepository menuRepository)
        {
            _menuTransactionRepository = menuTransactionRepository;
            _menuRepository = menuRepository;
        }

        public async Task<MenuTransaction> Execute(string menuLabel)
        {
            var menu = await _menuRepository.GetByLabel(menuLabel);

            var newMenuTransaction = await _menuTransactionRepository.Create(menu.MenuId);

            return newMenuTransaction;
        }
    }
}