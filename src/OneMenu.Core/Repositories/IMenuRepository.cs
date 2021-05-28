using System.Collections.Generic;
using System.Threading.Tasks;
using OneMenu.Core.Model;

namespace OneMenu.Core.Repositories
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAll();
        Task<Menu> GetByLabel(string menuLabel);
        Task<Menu> Get(string menuId);
    }
}