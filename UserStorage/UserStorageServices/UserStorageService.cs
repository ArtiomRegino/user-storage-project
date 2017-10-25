﻿using System;
using System.Collections.Generic;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService
    {
        public readonly List<User> Users;

        /// <summary>
        /// Create an instance of <see cref="UserStorageService"/>
        /// </summary>
        public UserStorageService()
        {
            Users = new List<User>();
        }

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
        public void Remove()
        {
            // TODO: Implement Remove() method.
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public void Search()
        {
            // TODO: Implement Search() method.
        }
    }
}
