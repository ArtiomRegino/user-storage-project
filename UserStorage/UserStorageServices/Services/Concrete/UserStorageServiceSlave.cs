using System;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Enums;
using UserStorageServices.Notifications;
using UserStorageServices.Repository.Interfaces;
using UserStorageServices.Services.Attributes;
using UserStorageServices.Services.Interfaces;

namespace UserStorageServices.Services.Concrete
{
    [Serializable]
    [MyApplicationService("UserStorageSlave")]
    public class UserStorageServiceSlave : UserStorageServiceBase, INotificationSubscriber
    {
        private object _lockState = new object();

        public UserStorageServiceSlave(IUserRepository repository) : base(repository)
        {
            var receiver = new NotificationReceiver();
            receiver.Received += NotificationReceived;
            Receiver = receiver;
        }

        public INotificationReceiver Receiver { get; }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.SlaveNode;

        public override void Add(User user)
        {
            lock (_lockState)
            {
                if (HaveMaster())
                {
                    base.Add(user);
                }
                else
                {
                    throw new NotSupportedException("This action is not allowed. Change service mode.");
                }
            }
        }

        public override bool Remove(User user)
        {
            lock (_lockState)
            {
                if (HaveMaster())
                {
                    return base.Remove(user);
                }

                throw new NotSupportedException("This action is not allowed. Change service mode.");
            }
        }

        public void UserAdded(User user)
        {
            Add(user);
        }

        public void UserRemoved(User user)
        {
            Remove(user);
        }

        private void NotificationReceived(NotificationContainer container)
        {
            foreach (var item in container.Notifications)
            {
                if (item.Type == NotificationType.AddUser)
                {
                    var user = ((AddUserActionNotification)item.Action).User;
                    Add(user);
                }
                else
                {
                    var user = ((DeleteUserActionNotification)item.Action).User;
                    Remove(user);
                }
            }
        }

        private bool HaveMaster()
        {
            var stackTrace = new StackTrace();
            var currentCalledOne = stackTrace.GetFrame(1).GetMethod();
            var currentCalledSecond = stackTrace.GetFrame(2).GetMethod();
            var calledMetodOne = typeof(UserStorageServiceMaster).GetMethod(currentCalledOne.Name);
            var calledMetodSecond = typeof(UserStorageServiceSlave).GetMethod(currentCalledSecond.Name);
            var frames = stackTrace.GetFrames();
            bool flag;

            if (frames != null)
            {
                flag = frames.Select(x => x.GetMethod()).Contains(calledMetodOne);

                if (!flag)
                {
                    flag = frames.Select(x => x.GetMethod()).Contains(calledMetodSecond);
                }
            }
            else
            {
                throw new InvalidOperationException();
            }

            return flag;
        }
    }
}
