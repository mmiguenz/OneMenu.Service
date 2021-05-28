using System.Collections.Generic;
using System.Linq;

namespace OneMenu.Core.Model
{
    public class Menu
    {
        public string MenuId { get; set; }
        public string Label { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public IEnumerable<Step> Steps { get; set; }

        public Step GetStepAt(int ordinal)
        {
            return Steps.First(s => s.Ordinal == ordinal);
        }
    }

}