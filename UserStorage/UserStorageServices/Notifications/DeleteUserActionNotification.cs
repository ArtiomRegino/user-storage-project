using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    public class DeleteUserActionNotification
    {
        [XmlElement("userId")]
        public User User { get; set; }
    }
}
