using System;
using CsStat.Domain.Entities;
using DataService.Interfaces;

namespace BusinessFacade.Repositories.Implementations
{
    public class ErrorLogRepository : BaseRepository, IErrorLogRepository 
    {
        public ErrorLogRepository(IMongoRepositoryFactory mongoRepository) : base(mongoRepository)
        {
        }

        public void Error(Error error)
        {
            base.Insert(error);
        }
    }
}