﻿using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.Enums;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Services
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public abstract class UserStorageServiceBase : IUserStorageService
    {
        private readonly List<User> _users;
        private readonly IUserIdGenerationService _generator;
        private readonly IValidator _validator;

        /// <summary>
        /// Create an instance of <see cref="UserStorageServiceBase"/>
        /// </summary>
        protected UserStorageServiceBase(IUserIdGenerationService generationService, IValidator validator)
        {
            _generator = generationService;
            _validator = validator;

            _users = new List<User>();
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => _users.Count;

        public abstract UserStorageServiceMode ServiceMode { get; }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public virtual void Add(User user)
        {
            _validator.Validate(user);
            user.Id = _generator.Generate();

            _users.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public virtual bool Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Id == Guid.Empty)
            {
                throw new ArgumentException("Id of user must be initialized", nameof(user));
            }

            return _users.Remove(user); 
        }

        /// <summary>
        /// Searches through the storage for a <see cref="IEnumerable&lt;User&gt;"/> that have such an age.
        /// </summary>
        public IEnumerable<User> SearchByAge(int age)
        {
            if (age <= 0 || age > 150)
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
            if (predicate == null)
            {
                throw new ArgumentNullException();
            }

            var currentUsers = _users.FirstOrDefault(u => predicate(u));

            return currentUsers;
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public IEnumerable<User> SearchByPredicate(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException();
            }

            var currentUsers = _users.Where(u => predicate(u));

            return currentUsers;
        } 
    }
}