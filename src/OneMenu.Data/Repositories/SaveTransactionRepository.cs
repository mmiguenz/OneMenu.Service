using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using OneMenu.Core.Repositories;



namespace OneMenu.Data.Repositories 
{
    public class SaveTransactionRepository : ISaveTransactionRepository
    {
        private readonly MongoClient _mongoClient;
        
        public SaveTransactionRepository(MongoClient monglcient)
        {
            _mongoClient = monglcient;
        }
        private IMongoCollection<BsonDocument> SaveTransactionCollection =>
            _mongoClient.GetDatabase("onemenu").GetCollection<BsonDocument>("saveTransaction");

        public async Task SaveTransaction(Dictionary<string, string> entity)
        {
            var bsonDocument = entity.ToBsonDocument();
            
            await SaveTransactionCollection.InsertOneAsync(bsonDocument);
        }
    }
}