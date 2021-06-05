using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using OneMenu.Core.Model;

namespace OneMenu.Core.Repositories
{
    public interface ISaveTransactionRepository

    {
        Task SaveTransaction(Dictionary<string, string> entity);
    }
}