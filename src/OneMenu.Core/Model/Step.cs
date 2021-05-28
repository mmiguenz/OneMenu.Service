using System.Collections.Generic;
using OneMenu.Core.Constants;

namespace OneMenu.Core.Model
{
    public record Step
    {
        public string Text { get; set; }
        public InputType InputType { get; set; }
        public int Ordinal { get; set; }
        public IEnumerable<Option> Options { get; set; }
        public bool IsLastStep { get; set; }

        public IEnumerable<string> Validate(string response)
        {
            return new List<string>();
        }
    }
}