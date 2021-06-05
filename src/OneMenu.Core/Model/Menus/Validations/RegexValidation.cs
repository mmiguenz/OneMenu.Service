using System;
using System.Text.RegularExpressions;

namespace OneMenu.Core.Model.Menus.Validations
{
    public class RegexValidation : Validation
    {
        private readonly Regex _expression;
        
        public RegexValidation(string errorMsj, string value, string type) : base(errorMsj, value, type)
        {
            _expression = new Regex(value,  RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public override string Validate(string response)
        {
            return ! _expression.IsMatch(response) ? ErrorMsj : string.Empty;
        }
    }
}