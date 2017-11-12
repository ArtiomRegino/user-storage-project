using System;

namespace UserStorageServices.Notifications
{
    public class NotificationReceiver : INotificationReceiver
    {
        public event Action<NotificationContainer> Received;

        public NotificationReceiver()
        {
            Received = container => { };
        }

        public void Receive(NotificationContainer container)
        {
            Received?.Invoke(container);
        }
    }
}
