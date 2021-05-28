using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OneMenu.Core.Model;

namespace OneMenu.Data.MongoModels
{
    [BsonIgnoreExtraElements]
    public class OptionModel
    {
        [BsonElement("displayText")]
        public string DisplayText { get; set; }
        [BsonElement("value")]
        public string Value { get; set; }
    }
}