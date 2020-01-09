using System.Collections.Generic;
using System.Linq;
using CsStat.Domain.Entities;
using DataService.Interfaces;
using MongoDB.Driver.Builders;

namespace BusinessFacade.Repositories.Implementations
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private IMongoRepositoryFactory _mongoRepository;
        public UserRepository(IMongoRepositoryFactory mongoRepository) : base(mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public void Add(User user)
        {
           base.Insert(user);
        }

        public User GetByName(string name)
        {
            var query = new QueryBuilder<User>();
            return _mongoRepository.GetRepository<User>().Collection.Find(query.EQ(x => x.Name, name.ToLower())).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
           return base.GetAll<User>();
        }
    }
}