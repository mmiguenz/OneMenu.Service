using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using OneMenu.Core.actions;
using OneMenu.Core.Constants;
using OneMenu.Core.Model;
using OneMenu.Core.Repositories;
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

            var menu = Data.Menu_Test;

            var menuTransaction = new MenuTransaction()
            {
                MenuId = menu.MenuId,
                MenuTransactionId = transactionId
            };
            
            _menuTransactionRepository
                .Setup(r => r.Get(transactionId))
                .ReturnsAsync((menuTransaction));
            
            _menuRepository
                .Setup(r => r.Get(menu.MenuId))
                .ReturnsAsync(menu);

            var result = await _getCurrentStepMenuTransaction.Execute(transactionId);
            
            Assert.NotNull(result);
            Assert.Equal(1, result.Ordinal);
            Assert.False(result.IsLastStep);
        }
        
        [Fact]
        public async Task When_Transaction_Exists_AndHasFirstAnswerd_Returns_NextNotAnsweredStep()
        {
            var transactionId = _fixture.Create<string>();
            var menu = Data.Menu_Test;

            var menuTransaction = new MenuTransaction()
            {
                MenuId = menu.MenuId,
                MenuTransactionId = transactionId,
                MenuStepAnswers = new List<MenuStepAnswer>()
                {
                    new MenuStepAnswer(menu.GetStepAt(1), "matias")
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
            var menu = Data.Menu_Test;
            var stepAnsweredWithErrors = new MenuStepAnswer(menu.GetStepAt(1), "matias");
            stepAnsweredWithErrors.ValidationErrors = new List<string>() {"Error1"};
            var menuTransaction = new MenuTransaction()
            {
                MenuId = menu.MenuId,
                MenuTransactionId = transactionId,
                MenuStepAnswers = new List<MenuStepAnswer>() { stepAnsweredWithErrors }
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
    
   

    public static class Data
    {
        private static readonly Fixture _fixture = new Fixture();
        
        private static readonly  Step step1 = new Step()
        {
            Text = "Ingrese su nombre",
            Ordinal = 1,
            InputType = InputType.TEXT,
            IsLastStep = false
        };

        private static readonly  Step step2 = new Step()
        {
            Text = "es graduado?",
            Ordinal = 2,
            InputType = InputType.OPTIONS,
            IsLastStep = false,
            Options = new List<Option>()
            {
                new Option()
                {
                    DisplayText = "SI",
                    Value = "SI"
                },
                new Option()
                {
                    DisplayText = "NO",
                    Value = "NO"
                }
            },
        };

        private static readonly  Step step3 = new Step()
        {
            Text = "ingrese numero de legajo",
            Ordinal = 3,
            InputType = InputType.TEXT,
            IsLastStep = true
        };
        
        public  static  Menu Menu_Test = new Menu()
        {
            MenuId = _fixture.Create<string>(),
            Label = "menu_test",
            Text = "this is menu test",
            Steps = new List<Step>()
            {
                step1,
                step2,
                step3
            }
        };
    }
    
    
}