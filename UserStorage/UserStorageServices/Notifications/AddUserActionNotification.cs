using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    class AddUserActionNotification
    {
        [XmlElement("user")]
        public User User { get; set; }
    }
}
