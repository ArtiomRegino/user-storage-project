using System;
using System.Diagnostics;
using System.Linq;
using UserStorageServices.Enums;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Services
{
    public class UserStorageServiceSlave : UserStorageServiceBase, INotificationSubscriber
    {
        public UserStorageServiceSlave(IUserIdGenerationService generator, IValidator validator) : base(generator, validator)
        {
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
            var stTrace = new StackTrace();
            var currentCalled = stTrace.GetFrame(1).GetMethod();
            var calledMetod = typeof(UserStorageServiceMaster).GetMethod(currentCalled.Name);
            var frames = stTrace.GetFrames();
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
