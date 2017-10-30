﻿using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.Interfaces;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService
    {
        public readonly List<User> Users;

        private readonly IUserIdGenerationService idGenerationService;

        /// <summary>
        /// Create an instance of <see cref="UserStorageService"/>
        /// </summary>
        public UserStorageService(IUserIdGenerationService idGenerationService)
        {
            this.idGenerationService = idGenerationService;
            Users = new List<User>();
        }

        public bool IsLoggingEnabled { get; set; }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count
        {
            get { return Users.Count; }
        }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("LastName or FirstName is null or empty or whitespace", nameof(user));
            }

            if (user.Age < 0)
            {
                throw new ArgumentException("Age is null less or equal to 0.", nameof(user));
            }

            Users.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public bool Remove(User user)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Remove() method is called.");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Id = idGenerationService.Generate();

            return Users.Remove(user); 
        }

        /// <summary>
        /// Searches through the storage for a <see cref="IEnumerable&lt;User&gt;"/> that have such an age.
        /// </summary>
        public IEnumerable<User> SearchByAge(int age)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByAge() method is called.");
            }

            if (age <= 0)
            {
                throw new ArgumentException("Age can't be less than 0.", nameof(age));
            }

            return SearchByPredicate(u => age == u.Age);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="IEnumerable&lt;User&gt;"/> that have such a last name.
        /// </summary>
        public IEnumerable<User> SearchByLastName(string lastName)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByLastName() method is called.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("FirstName is null or empty or whitespace", nameof(lastName));
            }

            return SearchByPredicate(u => lastName == u.LastName);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="IEnumerable&lt;User&gt;"/> that have such a first name.
        /// </summary>
        public IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByFirstName() method is called.");
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("FirstName is null or empty or whitespace", nameof(firstName));
            }

            return SearchByPredicate(u => firstName == u.FirstName);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public User SearchFirstByPredicate(Predicate<User> predicate)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchFirstByPredicate() method is called.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException();
            }

            var currentUsers = Users.FirstOrDefault(u => predicate(u));

            return currentUsers;
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public IEnumerable<User> SearchByPredicate(Predicate<User> predicate)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByPredicate() method is called.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException();
            }

            var currentUsers = Users.Where(u => predicate(u));

            return currentUsers;
        }
    }
}
