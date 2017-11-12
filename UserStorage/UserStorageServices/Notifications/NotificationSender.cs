namespace UserStorageServices.Notifications
{
    public class NotificationSender : INotificationSender
    {
        public INotificationReceiver Receiver { get; set; }

        public NotificationSender(INotificationReceiver receiver = null)
        {
            Receiver = receiver ?? new NotificationReceiver();
        }

        public void Send(NotificationContainer container)
        {
            Receiver.Receive(container);
        }
    }
}
