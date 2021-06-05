using System;
using System.Collections.Generic;
using System.Linq;
using OneMenu.Core.Model.Menus;

namespace OneMenu.Core.Model
{
    public class SaveStepResult
    {
        public SaveStepResult()
        {
            ValidationErrors = new List<string>();
        }
        public bool HasErrors => ValidationErrors.Any();
        public IEnumerable<string> ValidationErrors { get; set; }
        public  Step CurrentStep { get; set; }
        
        public string CompletionMsg { get; set; }

        public bool IsCompleted => !String.IsNullOrWhiteSpace(CompletionMsg);
    }
}