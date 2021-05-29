using System.Collections.Generic;
using System.Linq;

namespace OneMenu.Data.MongoModels
{
    public class MenuStepResponseModel
    {
        public  StepModel Step { get; set; }
        public  string Response { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
        public bool HasErrors => ValidationErrors.Any();
    }
}