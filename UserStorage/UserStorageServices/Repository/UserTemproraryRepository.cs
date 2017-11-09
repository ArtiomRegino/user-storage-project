using System;
using System.Collections.Generic;
using UserStorageServices.Interfaces;
using UserStorageServices.Services;

namespace UserStorageServices.Repository
{
    public class UserTemproraryRepository : IUserRepository
    {
        protected List<User> _users;
        protected readonly IUserIdGenerationService _generator;

        public UserTemproraryRepository(IUserIdGenerationService generationService = null)
        {
            _generator = generationService ?? new UserIdGenerationService();
            _users = new List<User>();
        }

        public int Count => _users.Count;

        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">Id of user.</param>
        /// <returns>Current user.</returns>
        public User Get(int id)
        {
            return _users.Find(u => u.Id == id);
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="user">Current user to delete.</param>
        /// <returns>Logic value (true - deleted).</returns>
        public bool Delete(User user)
        {
            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Id < 0)
            {
                throw new ArgumentException("Id of user must be more than zero.", nameof(user));
            }

            return _users.Remove(user);
        }

        /// <summary>
        /// Add userto repository.
        /// </summary>
        /// <param name="user">User to be added to repository.</param>
        public void Set(User user)
        {
            user.Id = _generator.Generate();

            _users.Add(user);
        }

        /// <summary>
        /// Gets a sequence of users by predicate.
        /// </summary>
        /// <param name="predicate">Predicate to get sequence of users.</param>
        /// <returns>Sequence of users.</returns>
        public IEnumerable<User> Query(Predicate<User> predicate)
        {
            return _users.FindAll(predicate);
        }
    }
}
