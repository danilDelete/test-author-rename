using System.Collections.Generic;
using CsStat.Domain.Entities;

namespace BusinessFacade.Repositories
{
    public interface IUsefulLinkRepository
    {
        void Add(UsefulInfo info);
        void Update(string id, UsefulInfo newInfo);
        void Remove(string id);
        IEnumerable<UsefulInfo> GetAll();
        UsefulInfo GetInfo(string id);
    }
}