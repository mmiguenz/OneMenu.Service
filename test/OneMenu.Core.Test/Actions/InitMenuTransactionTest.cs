using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using OneMenu.Core.actions;
using OneMenu.Core.Model;
using OneMenu.Core.Repositories;
using Xunit;

namespace OneMenu.Core.Test.Actions
{
    public class InitMenuTransactionTest
    {
        private readonly InitMenuTransaction _initMenuTransaction;
        private readonly Mock<IMenuRepository> _menuRepository;
        private readonly Mock<IMenuTransactionRepository> _menuTransactionRepository;
        private readonly Fixture _fixture;

        public InitMenuTransactionTest()
        {
            _menuRepository = new Mock<IMenuRepository>();
            _menuTransactionRepository = new Mock<IMenuTransactionRepository>();
            _initMenuTransaction = new InitMenuTransaction(_menuTransactionRepository.Object, _menuRepository.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Init_MenuTransaction_When_Success_Returns_TransactionCreated()
        {
            var menuLabel = _fixture.Create<string>();
            var menu = _fixture.Build<Menu>().With(m => m.Label, menuLabel).Create();

            _menuRepository.Setup(m => m.GetByLabel(menuLabel)).ReturnsAsync(menu);

            var expectedMenuTransactionCreated = _fixture.Build<MenuTransaction>()
                .With(mt => mt.MenuStepResponses, new List<MenuStepResponse>())
                .With(mt => mt.MenuId, menu.MenuId)
                .Create();

            _menuTransactionRepository.Setup(m => m.Create(menu.MenuId)).ReturnsAsync(expectedMenuTransactionCreated);

            var transactionIdCreated = await _initMenuTransaction.Execute(menuLabel);
            
            Assert.Equal(expectedMenuTransactionCreated.MenuTransactionId, transactionIdCreated.MenuTransactionId);
            Assert.Equal(expectedMenuTransactionCreated.MenuId, transactionIdCreated.MenuId);
            Assert.Empty(expectedMenuTransactionCreated.MenuStepResponses);
            Assert.False(expectedMenuTransactionCreated.IsCompleted);
        }
    }
}