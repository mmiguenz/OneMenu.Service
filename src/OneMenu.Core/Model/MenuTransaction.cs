using System.Collections.Generic;
using System.Linq;

namespace OneMenu.Core.Model
{
    public class MenuTransaction
    {
        public MenuTransaction()
        {
            MenuStepResponses = new List<MenuStepResponse>();
        }
        public string MenuTransactionId { get; set; }
        public string MenuId { get; set; }

        public IEnumerable<MenuStepResponse> MenuStepResponses { get; set; }

        public bool IsCompleted
        {
            get
            {
                return MenuStepResponses.Where(s =>!s.HasErrors)
                                      .Any(ms => ms.Step.IsLastStep);
            }
            
        }

        public int CurrentStepOrdinal
        {
            get
            {
                var stepOrdinals = MenuStepResponses?
                    .Where(m => !m.HasErrors)
                    .Select(m => m.Step.Ordinal);
                    
                var lastAnsweredStep = stepOrdinals.Any() ? stepOrdinals.Max() : 0;
                
                return IsCompleted ? lastAnsweredStep : lastAnsweredStep + 1;
            }
        }

        public void AddStepResponse(MenuStepResponse stepResponse)
        {
            var existingStep = MenuStepResponses.FirstOrDefault(sr => sr.Step.Ordinal == stepResponse.Step.Ordinal);
            MenuStepResponses = MenuStepResponses.Where(sr => sr != existingStep).Append(stepResponse);
        }
    }
}