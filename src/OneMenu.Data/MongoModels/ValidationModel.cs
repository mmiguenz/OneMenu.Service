using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OneMenu.Core.Model;

namespace OneMenu.Data.MongoModels
{
    [BsonIgnoreExtraElements]
    public class ValidationModel
    {
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("errorMsj")]
        public string ErrorMsj { get; set; }
        [BsonElement("value")]
        public string Value { get; set; }
    }
}