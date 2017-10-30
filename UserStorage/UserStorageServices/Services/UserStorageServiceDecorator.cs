using System;
using System.Collections.Generic;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Services
{
    public abstract class UserStorageServiceDecorator : IUserStorageService
    {
        protected readonly IUserStorageService StorageService;

        protected UserStorageServiceDecorator(IUserStorageService storageService)
        {
            StorageService = storageService;
        }

        public abstract int Count { get; }

        public abstract void Add(User user);

        public abstract bool Remove(User user);

        public abstract IEnumerable<User> SearchByAge(int age);

        public abstract IEnumerable<User> SearchByLastName(string lastName);

        public abstract IEnumerable<User> SearchByFirstName(string firstName);

        public abstract User SearchFirstByPredicate(Predicate<User> predicate);

        public abstract IEnumerable<User> SearchByPredicate(Predicate<User> predicate);
    }
}
