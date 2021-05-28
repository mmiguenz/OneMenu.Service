using System.Collections.Generic;
using System.Threading.Tasks;
using OneMenu.Core.Model;

namespace OneMenu.Core.Repositories
{
    public interface IMenuTransactionRepository
    {
        Task<MenuTransaction> Create(string menuMenuId);
        Task<MenuTransaction> Get(string transactionId);
        Task<MenuTransaction> Save(MenuTransaction menuTransaction);
    }
}