using System;
using System.Collections.Generic;
using System.Linq;

namespace OneMenu.Core.Model
{
    public class SaveStepResult
    {
        public bool Success => ValidationErrors.Any();
        public IEnumerable<string> ValidationErrors { get; set; }
        public  Step CurrentStep { get; set; }
        
        public string CompletionMsg { get; set; }

        public bool IsCompleted => !String.IsNullOrWhiteSpace(CompletionMsg);
    }
}