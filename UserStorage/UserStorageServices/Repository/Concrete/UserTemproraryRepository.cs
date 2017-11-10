using System;
using System.Collections.Generic;
using UserStorageServices.IdGenerators.Concrete;
using UserStorageServices.IdGenerators.Interfaces;
using UserStorageServices.Repository.Interfaces;

namespace UserStorageServices.Repository.Concrete
{
    public class UserTemproraryRepository : IUserRepository
    {
        protected readonly IUserIdGenerationService Generator;

        public UserTemproraryRepository(IUserIdGenerationService generationService = null)
        {
            Generator = generationService ?? new UserIdGenerationService();
            Users = new List<User>();
        }

        public int Count => Users.Count;

        protected List<User> Users { get; set; }

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
            return Users.Find(u => u.Id == id);
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

            return Users.Remove(user);
        }

        /// <summary>
        /// Add user to repository.
        /// </summary>
        /// <param name="user">User to be added to repository.</param>
        public void Set(User user)
        {
            user.Id = Generator.Generate();

            Users.Add(user);
        }

        /// <summary>
        /// Gets a sequence of users by predicate.
        /// </summary>
        /// <param name="predicate">Predicate to get sequence of users.</param>
        /// <returns>Sequence of users.</returns>
        public IEnumerable<User> Query(Predicate<User> predicate)
        {
            return Users.FindAll(predicate);
        }
    }
}
