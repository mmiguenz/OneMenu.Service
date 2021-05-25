using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OneMenu.Core.actions;
using OneMenu.Core.Model;
using MongoDB.Driver;
using OneMenu.Data.MongoModels;

namespace OneMenu.Data.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMapper _mapper;

        private IMongoCollection<MenuModel> MenuCollection =>
            _mongoClient.GetDatabase("onemenu").GetCollection<MenuModel>("menus");

        public MenuRepository(MongoClient mongoClient, IMapper mapper)
        {
            _mongoClient = mongoClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Menu>> GetAll()
        {
            var result = await (await MenuCollection.FindAsync<MenuModel>(_ => true)).ToListAsync();
            return _mapper.Map<List<MenuModel>, List<Menu>>(result);
        }
    }
}