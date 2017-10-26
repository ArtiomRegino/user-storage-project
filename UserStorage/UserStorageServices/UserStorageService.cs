using System;
using System.Collections.Generic;
using System.Linq;

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
        public bool Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Users.Remove(user); 
        }

        /// <summary>
        /// Searches through the storage for a <see cref="IEnumerable&lt;User&gt;"/> that have such an age.
        /// </summary>
        public IEnumerable<User> SearchByAge(int age)
        {
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

            return SearchByPredicate(u=> firstName == u.FirstName);
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

            var currentUsers = Users.FirstOrDefault(u => predicate(u));

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

            var currentUsers = Users.Where(u => predicate(u));

            return currentUsers;
        }
    }
}
