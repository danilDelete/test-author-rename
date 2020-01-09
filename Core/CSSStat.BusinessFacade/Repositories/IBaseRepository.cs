using System.Collections.Generic;
using MongoRepository;

namespace BusinessFacade.Repositories
{
    public interface IBaseRepository
    {
        void Insert<T>(T entity) where T : Entity;
        void InsertBatch<T>(IEnumerable<T> entities) where T : Entity;
        IEnumerable<T> GetAll<T>() where T : Entity;
        T GetOne<T>(string id) where T: Entity;
    }
}