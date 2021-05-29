using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OneMenu.Data.MongoModels
{
    [BsonIgnoreExtraElements]
    public class MenuTransactionModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MenuTransactionId { get; set; }
        [BsonElement("menuId")]
        public string MenuId { get; set; }
        [BsonElement("menuStepResponses")]
        public IEnumerable<MenuStepResponseModel> MenuStepResponses { get; set; }
        [BsonElement("isCompleted")]
        public bool IsCompleted { get; set; }
        [BsonElement("currentStepOrdinal")]
        public int CurrentStepOrdinal { get; set; }
    }
}