﻿using System;
using System.Collections.Generic;

namespace UserStorageServices.Interfaces
{
    public interface IUserStorageService
    {
        int Count { get; }

        void Add(User user);

        bool Remove(User user);

        IEnumerable<User> SearchByAge(int age);

        IEnumerable<User> SearchByLastName(string lastName);

        IEnumerable<User> SearchByFirstName(string firstName);

        User SearchFirstByPredicate(Predicate<User> predicate);

        IEnumerable<User> SearchByPredicate(Predicate<User> predicate);
    }
}