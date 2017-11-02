using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Enums;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Services
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService : IUserStorageService, INotificationSubscriber
    {
        public readonly List<User> Users;

        private readonly IUserIdGenerationService _generationService;
        private readonly IValidator _validator;
        private readonly UserStorageServiceMode _mode;
        private readonly IList<IUserStorageService> _slaveServices;
        private HashSet<INotificationSubscriber> _subscribers;

        /// <summary>
        /// Create an instance of <see cref="UserStorageService"/>
        /// </summary>
        public UserStorageService(IUserIdGenerationService generationService, IValidator validator,
            UserStorageServiceMode mode, IEnumerable<IUserStorageService> services = null)
        {
            if (services != null)
            {
                _slaveServices = services.ToList();
            }

            _mode = mode;
            _generationService = generationService;
            _validator = validator;
            _subscribers = new HashSet<INotificationSubscriber>();

            Users = new List<User>();
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => Users.Count;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (HaveMaster())
            {
                _validator.Validate(user);
                user.Id = _generationService.Generate();
                Users.Add(user);

                foreach (var item in _subscribers)
                {
                    item.UserAdded(user);
                }

                if (_slaveServices == null) return;

                foreach (var item in _slaveServices)
                {
                    item.Add(user);
                }
            }
            else 
            {
                throw new NotSupportedException("This action is not allowed. Change service mode.");
            }
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

            if (user.Id == Guid.Empty)
            {
                throw new ArgumentException("Id of user must be initialized", nameof(user));
            }

            bool flag;

            if (HaveMaster())
            {
                flag = Users.Remove(user);

                foreach (var item in _subscribers)
                {
                    item.UserRemoved(user);
                }

                if (_slaveServices == null) return flag;

                foreach (var item in _slaveServices)
                {
                    item.Remove(user);
                }
            }
            else
            {
                throw new NotSupportedException("This action is not allowed. Change service mode.");
            }

            return flag;
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

        public void UserAdded(User user)
        {
            Add(user);
        }

        public void UserRemoved(User user)
        {
            Remove(user);
        }

        public void AddSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            _subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            _subscribers.Remove(subscriber);
        }

        private bool HaveMaster()
        {
            var stTrace = new StackTrace();
            var currentCalled = stTrace.GetFrame(1).GetMethod();
            var frames = stTrace.GetFrames();
            int flag;
            if (frames != null)
            {
                flag = frames
                    .Select(x => x.GetMethod())
                    .Count(x => x == currentCalled);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return _mode == UserStorageServiceMode.MasterNode || flag >= 2;
        }

       
    }
}
