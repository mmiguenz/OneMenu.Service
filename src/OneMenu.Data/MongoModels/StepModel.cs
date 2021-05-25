using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OneMenu.Core.Model;

namespace OneMenu.Data.MongoModels
{
    [BsonIgnoreExtraElements]
    public class StepModel
    {
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("inputType")]
        public int InputType { get; set; }
        [BsonElement("ordinal")]
        public int Ordinal { get; set; }
        [BsonElement("isLastStep")]
        public bool IsLastStep { get; set; }
        [BsonIgnoreIfNull]
        [BsonElement("options")]
        public IEnumerable<OptionModel> Options { get; set; }
    }
}