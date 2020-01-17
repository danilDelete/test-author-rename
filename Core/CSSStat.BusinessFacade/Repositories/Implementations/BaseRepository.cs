using System.Collections.Generic;
using System.Linq;
using DataService.Interfaces;
using MongoDB.Driver.Builders;
using MongoRepository;

namespace BusinessFacade.Repositories.Implementations
{
    public class BaseRepository : IBaseRepository
    {
        private static IMongoRepositoryFactory _mongoRepository;

        public BaseRepository(IMongoRepositoryFactory mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public void Insert<T>(T entity) where T : Entity
        {
            _mongoRepository.GetRepository<T>().Collection.Insert(entity);
        }
        
        public void InsertBatch<T>(IEnumerable<T> entities) where T : Entity
        {
            if (entities != null && entities.Any())
                _mongoRepository.GetRepository<T>().Collection.InsertBatch(entities);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return _mongoRepository.GetRepository<T>().Collection.FindAll();
        }

        public T GetOne<T>(string id) where T : Entity
        {
            var query = new QueryBuilder<T>();
            return _mongoRepository.GetRepository<T>().Collection.Find(query.EQ(x => x.Id, id)).FirstOrDefault();
        }
    }
}