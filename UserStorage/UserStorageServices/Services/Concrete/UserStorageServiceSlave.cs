using System;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Enums;
using UserStorageServices.Repository.Interfaces;
using UserStorageServices.Services.Interfaces;

namespace UserStorageServices.Services.Concrete
{
    public class UserStorageServiceSlave : UserStorageServiceBase, INotificationSubscriber
    {
        public UserStorageServiceSlave(IUserRepository repository) : base(repository)
        {
        }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.SlaveNode;

        public override void Add(User user)
        {
            if (this.HaveMaster())
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
            if (this.HaveMaster())
            {
                return base.Remove(user);
            }

            throw new NotSupportedException("This action is not allowed. Change service mode.");
        }

        public void UserAdded(User user)
        {
            this.Add(user);
        }

        public void UserRemoved(User user)
        {
            this.Remove(user);
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
