using System.Collections.Generic;
using System.Linq;

namespace OneMenu.Core.Model
{
    public class MenuStepAnswer
    {
        public MenuStepAnswer(Step step, string response)
        {
            Step = step;
            ValidationErrors = step.Validate(response);
        }
        public  Step Step { get; set; }
        public  string Response { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
        public bool HasErrors => ValidationErrors.Any();

    }
}