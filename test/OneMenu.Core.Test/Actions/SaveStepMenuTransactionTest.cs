using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using OneMenu.Core.actions;
using OneMenu.Core.CompletionCommands;
using OneMenu.Core.Constants;
using OneMenu.Core.Model;
using OneMenu.Core.Model.Menus;
using OneMenu.Core.Repositories;
using OneMenu.Core.Test.util;
using Xunit;

namespace OneMenu.Core.Test.Actions
{
    public class SaveStepMenuTransactionTest
    {
        private readonly SaveStepMenuTransaction _saveStepMenuTransaction;
        private readonly Mock<IMenuRepository> _menuRepository;
        private readonly Mock<IMenuTransactionRepository> _menuTransactionRepository;
        private readonly Mock<ITransactionCompleteCommand> _transactionCompletedCommand;
        private readonly Fixture _fixture;

        public SaveStepMenuTransactionTest()
        {
            _menuRepository = new Mock<IMenuRepository>();
            _menuTransactionRepository = new Mock<IMenuTransactionRepository>();
            _transactionCompletedCommand = new Mock<ITransactionCompleteCommand>();
            
            var completionCommands = new List<ITransactionCompleteCommand>()
            {
                _transactionCompletedCommand.Object
            };
            
            _saveStepMenuTransaction =
                new SaveStepMenuTransaction(_menuTransactionRepository.Object, _menuRepository.Object, completionCommands);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task When_Save_FirstStep_With_Success()
        {
            var transactionId = _fixture.Create<string>();
            var stepResponse = _fixture.Create<string>();
            var menu = MenuData.Menu_Test;

            var menuTransaction = new MenuTransaction()
            {
                MenuId = menu.MenuId,
                MenuTransactionId = transactionId
            };

            SetupMenuTransactionRepository_Get(transactionId, menuTransaction);
            SetupMenuRepository_Get(menu);
            SetupCompletionCommand(menu, menuTransaction);

            var result = await _saveStepMenuTransaction.Execute(transactionId, stepResponse);

            Assert.False(result.HasErrors);
            Assert.False(result.IsCompleted);
            Assert.Empty(result.CompletionMsg);
            StepsAreEqual(MenuData.step2, result.CurrentStep);
        }
        [Fact]
        public async Task When_Save_LastStep_With_Success()
        {
            var transactionId = _fixture.Create<string>();
            var lastStepResponse = _fixture.Create<int>().ToString();
            var menu = MenuData.Menu_Test;

            var menuTransaction = new MenuTransaction()
            {
                MenuId = menu.MenuId,
                MenuTransactionId = transactionId,
                MenuStepResponses = new List<MenuStepResponse>()
                {
                    new(MenuData.step1, _fixture.Create<string>()),
                    new(MenuData.step2, _fixture.Create<string>())
                }
            };

            SetupMenuTransactionRepository_Get(transactionId, menuTransaction);
            SetupMenuRepository_Get(menu);
            SetupCompletionCommand(menu, menuTransaction);

            var result = await _saveStepMenuTransaction.Execute(transactionId, lastStepResponse);

            Assert.False(result.HasErrors);
            Assert.True(result.IsCompleted);
            Assert.NotEmpty(result.CompletionMsg);
            Assert.Null(result.CurrentStep);
        }

        [Fact]
        public async Task When_Save_FirstStep_With_ValidationErrors()
        {
            var transactionId = _fixture.Create<string>();
            var stepInvalidResponse = "AAA";
            var menu = MenuData.Menu_Test;

            var menuTransaction = new MenuTransaction()
            {
                MenuId = menu.MenuId,
                MenuTransactionId = transactionId,
                MenuStepResponses = new List<MenuStepResponse>()
                {
                    new () { Step =  MenuData.step1, Response =  "response step 1"},
                    new () { Step =  MenuData.step2, Response =  "response step 2"},
                }
            };

            SetupMenuTransactionRepository_Get(transactionId, menuTransaction);
            SetupMenuRepository_Get(menu);

            var result = await _saveStepMenuTransaction.Execute(transactionId, stepInvalidResponse);

            Assert.True(result.HasErrors);
            Assert.False(result.IsCompleted);
            Assert.Empty(result.CompletionMsg);
            Assert.NotEmpty(result.ValidationErrors);
            StepsAreEqual(MenuData.step3, result.CurrentStep);
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
                .ReturnsAsync((MenuTransaction) menuTransaction);
        }
        
        private void SetupCompletionCommand(Menu menu, MenuTransaction menuTransaction)
        {
            _transactionCompletedCommand.Setup(t => t.TransactionCompleteType)
                .Returns(menu.TransactionCompleteCommand);
            
            _transactionCompletedCommand.Setup(t => t.CompleteTransaction(menuTransaction))
                .ReturnsAsync("Transaction Completed");
        }

        private void StepsAreEqual(Step expectedStep, Step currentStep)
        {
            Assert.Equal(expectedStep.Text, currentStep.Text);
            Assert.Equal(expectedStep.Ordinal, currentStep.Ordinal);
            Assert.Equal(expectedStep.InputType, currentStep.InputType);
            Assert.Equal(expectedStep.IsLastStep, currentStep.IsLastStep);
        }
    }
}