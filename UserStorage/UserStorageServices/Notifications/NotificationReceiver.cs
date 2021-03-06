﻿using System;
using System.IO;
using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    [Serializable]
    public class NotificationReceiver : INotificationReceiver
    {
        public NotificationReceiver()
        {
            Received = xmlContainer => { };
        }

        public event Action<NotificationContainer> Received;

        public void Receive(string xmlContainer)
        {
            NotificationContainer container;

            using (var stringReader = new StringReader(xmlContainer))
            {
                var serializer = new XmlSerializer(typeof(NotificationContainer));

                container = (NotificationContainer)serializer.Deserialize(stringReader);
            }

            Received?.Invoke(container);
        }
    }
}
