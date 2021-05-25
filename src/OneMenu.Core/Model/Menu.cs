using System.Collections.Generic;

namespace OneMenu.Core.Model
{
    public class Menu
    {
        public string Label { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public IEnumerable<Step> Steps { get; set; }
        
    }
}