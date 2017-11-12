using System;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Enums;
using UserStorageServices.Notifications;
using UserStorageServices.Repository.Interfaces;
using UserStorageServices.Services.Interfaces;

namespace UserStorageServices.Services.Concrete
{
    public class UserStorageServiceSlave : UserStorageServiceBase, INotificationSubscriber
    {
        public INotificationReceiver Receiver { get; }

        public UserStorageServiceSlave(IUserRepository repository) : base(repository)
        {
            var receiver = new NotificationReceiver();
            receiver.Received += NotificationReceived;
            Receiver = receiver;
        }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.SlaveNode;

        public override void Add(User user)
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

        public override bool Remove(User user)
        {
            if (HaveMaster())
            {
                return base.Remove(user);
            }

            throw new NotSupportedException("This action is not allowed. Change service mode.");
        }

        private void NotificationReceived(NotificationContainer container)
        {
            foreach (var item in container.Notifications)
            {
                if (item.Type == NotificationType.AddUser)
                {
                    var user = ((AddUserActionNotification) item.Action).User;
                    Add(user);
                }
                else
                {
                    var user = ((DeleteUserActionNotification)item.Action).User;
                    Remove(user);
                }
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

        private bool HaveMaster()
        {
            var stackTrace = new StackTrace();
            var currentCalled = stackTrace.GetFrame(1).GetMethod();
            var calledMetod = typeof(UserStorageServiceMaster).GetMethod(currentCalled.Name);
            var frames = stackTrace.GetFrames();
            bool flag;
            if (frames != null)
            {
                flag = frames.Select(x => x.GetMethod()).Contains(calledMetod);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return flag;
        }
    }
}
