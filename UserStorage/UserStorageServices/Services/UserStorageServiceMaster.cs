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

        public UserStorageServiceMaster(IUserIdGenerationService generationService, IValidator validator, IEnumerable<IUserStorageService> services = null)
            : base(generationService, validator)
        {
            this._slaveServices = services?.ToList() ?? new List<IUserStorageService>();
            this._subscribers = new HashSet<INotificationSubscriber>();
        }

        private event Action<User> AddedToStorage;

        private event Action<User> RemoveedFromStorage;

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.MasterNode;

        public override void Add(User user)
        {
            base.Add(user);

            this.OnUserAdded(user);

            foreach (var item in this._slaveServices)
            {
                item.Add(user);
            }
        }

        public override bool Remove(User user)
        {
            var flag = base.Remove(user);

            this.OnUserRemoved(user);

            foreach (var item in this._slaveServices)
            {
                item.Remove(user);
            }

            return flag;
        }

        public void AddSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            this._subscribers.Add(subscriber);
            this.AddedToStorage += subscriber.UserAdded;
            this.RemoveedFromStorage += subscriber.UserRemoved;
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            this._subscribers.Remove(subscriber);
            this.AddedToStorage -= subscriber.UserAdded;
            this.RemoveedFromStorage -= subscriber.UserRemoved;
        }

        private void OnUserAdded(User user)
        {
            this.AddedToStorage?.Invoke(user);
        }

        private void OnUserRemoved(User user)
        {
            this.RemoveedFromStorage?.Invoke(user);
        }
    }
}