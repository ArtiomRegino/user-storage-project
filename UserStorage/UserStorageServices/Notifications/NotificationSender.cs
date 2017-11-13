using System;
using System.IO;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    [Serializable]
    public class NotificationSender : INotificationSender
    {
        public NotificationSender(INotificationReceiver receiver = null)
        {
            Receiver = receiver ?? new NotificationReceiver();
        }

        public INotificationReceiver Receiver { get; set; }

        public XmlSerializer Serializer { get; set; }

        public void AddReceiver(INotificationReceiver receiver)
        {
            if (receiver == null)
            {
                throw new ArgumentNullException(nameof(receiver));
            }

            Receiver = receiver;
        }

        public void Send(NotificationContainer container)
        {
            using (var stringWriter = new StringWriter())
            {
                Serializer = new XmlSerializer(typeof(NotificationContainer));

                Serializer.Serialize(stringWriter, container);

                Receiver.Receive(stringWriter.ToString());
            }
        }
    }
}
