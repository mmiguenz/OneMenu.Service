using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OneMenu.Core.Model;

namespace OneMenu.Data.MongoModels
{
    [BsonIgnoreExtraElements]
    public class OptionModel
    {
        public string DisplayText { get; set; }
        public string Value { get; set; }
    }
}