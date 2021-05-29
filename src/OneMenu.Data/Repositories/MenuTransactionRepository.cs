using System.Threading.Tasks;
using AutoMapper;
using OneMenu.Core.Model;
using MongoDB.Driver;
using OneMenu.Core.Repositories;
using OneMenu.Data.MongoModels;

namespace OneMenu.Data.Repositories
{
    public class MenuTransactionRepository : IMenuTransactionRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMapper _mapper;

        private IMongoCollection<MenuTransactionModel> MenuTransactionCollection =>
            _mongoClient
                .GetDatabase("onemenu")
                .GetCollection<MenuTransactionModel>("menuTransaction");

        public MenuTransactionRepository(MongoClient mongoClient, IMapper mapper)
        {
            _mongoClient = mongoClient;
            _mapper = mapper;
        }

        public async Task<MenuTransaction> Create(string menuMenuId)
        {
            var menuTransaction = new MenuTransactionModel()
            {
                MenuId = menuMenuId
            };

            await MenuTransactionCollection.InsertOneAsync(menuTransaction);

            return _mapper.Map<MenuTransactionModel, MenuTransaction>(menuTransaction);
        }

        public async Task<MenuTransaction> Get(string transactionId)
        {
            var result = await (await MenuTransactionCollection.FindAsync(m => m.MenuTransactionId == transactionId))
                .FirstAsync();
            return _mapper.Map<MenuTransactionModel, MenuTransaction>(result);
        }

        public async Task<MenuTransaction> Save(MenuTransaction menuTransaction)
        {
            var menuTransactionToUpdate = _mapper.Map<MenuTransaction, MenuTransactionModel>(menuTransaction);
            var result = await MenuTransactionCollection
                .FindOneAndReplaceAsync(d => d.MenuTransactionId == menuTransaction.MenuTransactionId,
                    menuTransactionToUpdate);

            return _mapper.Map<MenuTransactionModel, MenuTransaction>(result);
        }
    }
}