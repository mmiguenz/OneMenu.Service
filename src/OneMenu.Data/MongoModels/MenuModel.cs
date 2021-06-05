using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OneMenu.Core.Model;

namespace OneMenu.Data.MongoModels
{
    [BsonIgnoreExtraElements]
    public class MenuModel
    {  
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MenuId { get; set; }
        [BsonElement("label")]
        public string Label { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("completionCommand")]
        public string TransactionCompleteCommand { get; set; }
        [BsonElement("steps")]
        public IEnumerable<StepModel> Steps { get; set; }
    }
}