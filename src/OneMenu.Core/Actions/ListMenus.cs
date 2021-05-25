using System.Collections.Generic;
using System.Threading.Tasks;
using OneMenu.Core.Model;

namespace OneMenu.Core.actions
{
    public class ListMenus
    {
        private readonly IMenuRepository _menuRepository;

        public ListMenus(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<IEnumerable<Menu>> Execute()
        {
            var menus = await _menuRepository.GetAll();
            return menus;
        }
    }

    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAll();
    }
}