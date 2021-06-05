using System.Threading.Tasks;
using OneMenu.Core.Model;

namespace OneMenu.Core.CompletionCommands
{
    public interface ITransactionCompleteCommand
    {
        string TransactionCompleteType
        {
            get;
        }
        
        Task<string> CompleteTransaction(MenuTransaction menuTransaction);
    }
}