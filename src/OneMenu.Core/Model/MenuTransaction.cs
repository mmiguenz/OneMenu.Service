using System.Collections.Generic;
using System.Linq;

namespace OneMenu.Core.Model
{
    public class MenuTransaction
    {
        public MenuTransaction()
        {
            MenuStepAnswers = new List<MenuStepAnswer>();
        }
        public string MenuTransactionId { get; set; }
        public string MenuId { get; set; }

        public IEnumerable<MenuStepAnswer> MenuStepAnswers { get; set; }

        public bool IsCompleted
        {
            get
            {
                return MenuStepAnswers.Any(ms => ms.Step.IsLastStep);

            }
            
        }

        public int CurrentStepOrdinal
        {
            get
            {
                var stepOrdinals = MenuStepAnswers?
                    .Where(m => !m.HasErrors)
                    .Select(m => m.Step.Ordinal);
                    
                var lastAnsweredStep = stepOrdinals.Any() ? stepOrdinals.Max() : 0;
                
                return lastAnsweredStep + 1;
            }
        }
    }
}