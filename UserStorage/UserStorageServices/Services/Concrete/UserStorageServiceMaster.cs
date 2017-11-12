using System;
using System.Collections.Generic;
using System.Linq;
using UserStorageServices.Enums;
using UserStorageServices.Notifications;
using UserStorageServices.Repository.Interfaces;
using UserStorageServices.Services.Interfaces;
using UserStorageServices.Validators.Concrete;
using UserStorageServices.Validators.Interfaces;

namespace UserStorageServices.Services.Concrete
{
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        private readonly IEnumerable<IUserStorageService> _slaveServices;
        private readonly HashSet<INotificationSubscriber> _subscribers;
        private readonly IValidator _validator;

        public UserStorageServiceMaster(IUserRepository repository, IValidator validator = null, IEnumerable<IUserStorageService> services = null)
            : base(repository)
        {
            _validator = validator ?? new CompositeValidator();
            _slaveServices = services?.ToList() ?? new List<IUserStorageService>();
            _subscribers = new HashSet<INotificationSubscriber>();
            Sender = new NotificationSender();
        }

        private event Action<User> AddedToStorage;

        private event Action<User> RemoveedFromStorage;

        public NotificationSender Sender { get; }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.MasterNode;

        public override void Add(User user)
        {
            _validator.Validate(user);
            base.Add(user);

            OnUserAdded(user);

            foreach (var item in _slaveServices)
            {
                item.Add(user);
            }

            Sender.Send(new NotificationContainer
            {
                Notifications = new[]
                {
                    new Notification
                    {
                        Type = NotificationType.AddUser,
                        Action = new AddUserActionNotification
                        {
                            User = user
                        }
                    }
                }
            });
        }

        public override bool Remove(User user)
        {
            var flag = base.Remove(user);

            OnUserRemoved(user);

            foreach (var item in _slaveServices)
            {
                item.Remove(user);
            }

            Sender.Send(new NotificationContainer
            {
                Notifications = new[]
                {
                    new Notification
                    {
                        Type = NotificationType.DeleteUser,
                        Action = new DeleteUserActionNotification
                        {
                            User = user
                        }
                    }
                }
            });

            return flag;
        }

        public void AddSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            _subscribers.Add(subscriber);
            AddedToStorage += subscriber.UserAdded;
            RemoveedFromStorage += subscriber.UserRemoved;
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            _subscribers.Remove(subscriber);
            AddedToStorage -= subscriber.UserAdded;
            RemoveedFromStorage -= subscriber.UserRemoved;
        }

        private void OnUserAdded(User user)
        {
            AddedToStorage?.Invoke(user);
        }

        private void OnUserRemoved(User user)
        {
            RemoveedFromStorage?.Invoke(user);
        }
    }
}
