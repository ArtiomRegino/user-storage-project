using System;
using System.Collections.Generic;

namespace UserStorageServices.Repository.Interfaces
{
    public interface IUserRepository
    {
        int Count { get; }

        User Get(int id);

        bool Delete(User user);

        void Set(User user);

        IEnumerable<User> Query(Predicate<User> predicate);
    }
}
