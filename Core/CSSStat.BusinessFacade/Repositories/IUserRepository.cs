using System;
using System.Collections.Generic;
using CsStat.Domain.Entities;

namespace BusinessFacade.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetByName(string name);
        IEnumerable<User> GetAll();
    }
}