﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Enums;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Services
{
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        private readonly IEnumerable<IUserStorageService> _slaveServices;
        private readonly HashSet<INotificationSubscriber> _subscribers;

        private event Action<User> AddedToStorage;
        private event Action<User> RemoveedFromStorage;

        public UserStorageServiceMaster(IUserIdGenerationService generationService, IValidator validator, IEnumerable<IUserStorageService> services = null)
            : base(generationService, validator)
        {
            _slaveServices = services?.ToList() ?? new List<IUserStorageService>();
            _subscribers = new HashSet<INotificationSubscriber>();
        }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.MasterNode;

        public override void Add(User user)
        {
            base.Add(user);

            OnUserAdded(user);

            foreach (var item in _slaveServices)
            {
                item.Add(user);
            }
        }

        public override bool Remove(User user)
        {
            var flag = base.Remove(user);

            OnUserRemoved(user);

            foreach (var item in _slaveServices)
            {
                item.Remove(user);
            }

            return flag;
        }

        private void OnUserAdded(User user)
        {
            AddedToStorage?.Invoke(user);
        }

        private void OnUserRemoved(User user)
        {
            RemoveedFromStorage?.Invoke(user);
        }

        public void AddSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            _subscribers.Add(subscriber);
            AddedToStorage += subscriber.UserAdded;
            RemoveedFromStorage += subscriber.UserRemoved;
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            _subscribers.Remove(subscriber);
            AddedToStorage -= subscriber.UserAdded;
            RemoveedFromStorage -= subscriber.UserRemoved;
        }
    }
}
