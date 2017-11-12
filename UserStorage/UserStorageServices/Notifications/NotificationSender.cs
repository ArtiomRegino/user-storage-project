using System.IO;
using System.Xml.Serialization;

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
            using (var stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(NotificationContainer));

                serializer.Serialize(stringWriter, container);

                Receiver.Receive(stringWriter.ToString());
            }
        }
    }
}
