using System;
using System.Collections.Generic;

namespace UserStorageServices.Repository
{
    public interface IUserRepository
    {
        int Count { get; }

        void Start();

        void Stop();

        User Get(Guid id);

        bool Delete(User user);

        void Set(User user);

        IEnumerable<User> Query(Predicate<User> predicate);
    }
}
