using System.Collections.Generic;
using System.Linq;
using OneMenu.Core.Constants;

namespace OneMenu.Core.Model.Menus
{
    public class Step
    {
        public Step()
        {
            Validations = new List<Validation>();
            Options = new List<Option>();
        }
        
        public string Text { get; set; }
        public string FieldName { get; set; }
        public string InputType { get; set; }
        public int Ordinal { get; set; }
        public IEnumerable<Option> Options { get; set; }
        public IEnumerable<Validation> Validations { get; set; }
        public bool IsLastStep { get; set; }

        public IEnumerable<string> Validate(string response)
        {
            return Validations.Select(v => v.Validate(response)).Where(vm => !string.IsNullOrWhiteSpace(vm));
        }
    }
}