namespace UserStorageServices.Notifications
{
    public interface INotificationReceiver
    {
        void Receive(NotificationContainer container);
    }
}
