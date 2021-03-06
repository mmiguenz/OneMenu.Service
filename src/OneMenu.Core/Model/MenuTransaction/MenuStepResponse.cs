using System.Collections.Generic;
using System.Linq;
using OneMenu.Core.Model.Menus;

namespace OneMenu.Core.Model
{
    public class MenuStepResponse
    {
        public MenuStepResponse()
        {
            ValidationErrors = new List<string>();
        }
        public MenuStepResponse(Step step, string response)
        {
            Step = step;
            Response = response;
            ValidationErrors = step.Validate(response);
        }
        public  Step Step { get; set; }
        public  string Response { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
        public bool HasErrors => ValidationErrors.Any();

    }
}