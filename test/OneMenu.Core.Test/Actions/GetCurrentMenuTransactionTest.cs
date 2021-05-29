using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using OneMenu.Core.actions;
using OneMenu.Core.Constants;
using OneMenu.Core.Model;
using OneMenu.Core.Repositories;
using OneMenu.Core.Test.util;
using Xunit;

namespace OneMenu.Core.Test.Actions
{
    public class GetCurrentStepMenuTransactionTest
    {
        private readonly GetCurrentStepMenuTransaction _getCurrentStepMenuTransaction;
        private readonly Mock<IMenuRepository> _menuRepository;
        private readonly Mock<IMenuTransactionRepository> _menuTransactionRepository;
        private readonly Fixture _fixture;

        public GetCurrentStepMenuTransactionTest()
        {
            _menuRepository = new Mock<IMenuRepository>();
            _menuTransactionRepository = new Mock<IMenuTransactionRepository>();
            _getCurrentStepMenuTransaction = new GetCurrentStepMenuTransaction(_menuTransactionRepository.Object, _menuRepository.Object);
            _fixture = new Fixture();
        }
        
        [Fact]
        public async Task When_Transaction_Exists_AndHasNoAnswersYet_Returns_FirstStep()
        {
            var transactionId = _fixture.Create<string>();

            var menu = MenuData.Menu_Test;

            var menuTransaction = new MenuTransaction()
            {
                MenuId = menu.MenuId,
                MenuTransactionId = transactionId
            };
            
            SetupMenuTransactionRepository_Get(transactionId, menuTransaction);
            SetupMenuRepository_Get(menu);

            var result = await _getCurrentStepMenuTransaction.Execute(transactionId);
            
            Assert.NotNull(result);
            Assert.Equal(1, result.Ordinal);
            Assert.False(result.IsLastStep);
        }
        
        [Fact]
        public async Task When_Transaction_Exists_AndHasFirstAnswerd_Returns_NextNotAnsweredStep()
        {
            var transactionId = _fixture.Create<string>();
            var menu = MenuData.Menu_Test;

            var menuTransaction = new MenuTransaction()
            {
                MenuId = menu.MenuId,
                MenuTransactionId = transactionId,
                MenuStepResponses = new List<MenuStepResponse>()
                {
                    new (menu.GetStepAt(1), "matias")
                }
            };
            
            SetupMenuTransactionRepository_Get(transactionId, menuTransaction);
            SetupMenuRepository_Get(menu);
            
            var result = await _getCurrentStepMenuTransaction.Execute(transactionId);
            
            Assert.NotNull(result);
            Assert.Equal(2, result.Ordinal);
            Assert.False(result.IsLastStep);
        }
        
        [Fact]
        public async Task When_Transaction_Exists_AndHasFirstAnswered_WithErrors_Returns_FirstStep()
        {
            var transactionId = _fixture.Create<string>();
            var menu = MenuData.Menu_Test;
            var stepAnsweredWithErrors = new MenuStepResponse(menu.GetStepAt(1), "matias");
            stepAnsweredWithErrors.ValidationErrors = new List<string>() {"Error1"};
            var menuTransaction = new MenuTransaction()
            {
                MenuId = menu.MenuId,
                MenuTransactionId = transactionId,
                MenuStepResponses = new List<MenuStepResponse>() { stepAnsweredWithErrors }
            };
            
            SetupMenuTransactionRepository_Get(transactionId, menuTransaction);
            SetupMenuRepository_Get(menu);

            var result = await _getCurrentStepMenuTransaction.Execute(transactionId);
            
            Assert.NotNull(result);
            Assert.Equal(1, result.Ordinal);
            Assert.False(result.IsLastStep);
        }

        [Fact]
        public async Task When_Transaction_Does_Not_Exists_Returns_Null()
        {
            var transactionId = _fixture.Create<string>();
            
            SetupMenuTransactionRepository_Get(transactionId, null);

            var result = await _getCurrentStepMenuTransaction.Execute(transactionId);
            
            Assert.Null(result);
        }
        
        private void SetupMenuRepository_Get(Menu menuToReturn) 
        {
            _menuRepository
                .Setup(r => r.Get(menuToReturn.MenuId))
                .ReturnsAsync(menuToReturn);
        }
        
        private void SetupMenuTransactionRepository_Get(string transactionId, MenuTransaction menuTransaction) 
        {
            _menuTransactionRepository
                .Setup(r => r.Get(transactionId))
                .ReturnsAsync((MenuTransaction)menuTransaction);
        }
    }
}