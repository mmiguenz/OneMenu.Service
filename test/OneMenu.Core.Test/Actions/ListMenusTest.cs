using System;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using OneMenu.Core.actions;
using OneMenu.Core.Model;
using Xunit;

namespace OneMenu.Core.Test.Actions
{
    public class ListMenusTest
    {
        private readonly ListMenus _listMenusAction;
        private readonly Mock<IMenuRepository> _menuRepository;
        private readonly Fixture _fixture = new Fixture();

        public ListMenusTest()
        {
            _menuRepository = new Mock<IMenuRepository>();
            _listMenusAction = new ListMenus(_menuRepository.Object);
        }
        [Fact]
        public async Task ListMenus_Returns_A_ListOf_AvailableMenus()
        {
            var menusToReturn = _fixture.CreateMany<Menu>();
            
            _menuRepository.Setup(m => m.GetAll()).ReturnsAsync(menusToReturn);
            
            var result = await _listMenusAction.Execute();
            
            Assert.NotEmpty(result);
        }
    }
}