using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using OneMenu.Core.actions;
using OneMenu.Core.Model;
using OneMenu.Core.Repositories;
using OneMenu.Core.Test.util;
using Xunit;

namespace OneMenu.Core.Test.Actions
{
    public class ListMenusTest
    {
        private readonly ListMenus _listMenusAction;
        private readonly Mock<IMenuRepository> _menuRepository;
        private readonly Fixture _fixture;

        public ListMenusTest()
        {
            _menuRepository = new Mock<IMenuRepository>();
            _listMenusAction = new ListMenus(_menuRepository.Object);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task ListMenus_Returns_A_ListOf_AvailableMenus()
        {
            var menusToReturn = new List<Menu>() {MenuData.Menu_Test};
            
            _menuRepository.Setup(m => m.GetAll()).ReturnsAsync(menusToReturn);
            
            var result = await _listMenusAction.Execute();
            
            Assert.NotEmpty(result);
        }
    }
}