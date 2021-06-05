namespace OneMenu.Core.Model
{
    public abstract class Validation
    {
        private readonly string _type;
        private readonly string _errorMsj;
        private readonly string _value;
        public string ErrorMsj => _errorMsj;
        public string Value => _value;
        public string Type => _type;

        public Validation(string errorMsj, string value, string type)
        {
            _errorMsj = errorMsj;
            _value = value;
            _type = type;
        }

        public abstract string Validate(string response);
    }
}