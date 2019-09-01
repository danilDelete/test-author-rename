﻿using System.Collections.Generic;
using MongoRepository;

namespace BusinessFacade.Repositories
{
    public interface IBaseRepository
    {
        void InsertLog<T>(T entity) where T : Entity;

        void InsertBatch<T>(IEnumerable<T> entities) where T : Entity;
    }
}