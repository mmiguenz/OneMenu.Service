using System.Collections.Generic;
using AutoFixture;
using OneMenu.Core.Constants;
using OneMenu.Core.Model;
using OneMenu.Core.Model.Menus;
using OneMenu.Core.Model.Menus.Validations;
using OnstringeMenu.Core.Constants;

namespace OneMenu.Core.Test.util
{
    public static class MenuData
    {
        private static readonly Fixture _fixture = new Fixture();
        
        public static readonly  Step step1 = new Step()
        {
            Text = "Ingrese su nombre",
            Ordinal = 1,
            InputType = InputType.TEXT,
            IsLastStep = false
        };

        public static readonly  Step step2 = new Step()
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

        public static readonly  Step step3 = new Step()
        {
            Text = "ingrese numero de legajo",
            Ordinal = 3,
            InputType = InputType.TEXT,
            IsLastStep = true,
            Validations = new List<Validation>() { new RegexValidation("debe ingresar un valor numerico", "^[0-9]*$", ValidationType.Regex) }
        };


        public  static  Menu Menu_Test = new Menu()
        {
            MenuId = _fixture.Create<string>(),
            Label = "menu_test",
            Text = "this is menu test",
            TransactionCompleteCommand = TransactionCompleteCommandType.SaveEntity,
            Steps = new List<Step>()
            {
                step1,
                step2,
                step3
            }
        };
    }
}