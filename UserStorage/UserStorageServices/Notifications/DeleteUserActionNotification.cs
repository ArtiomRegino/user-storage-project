using System.Xml.Serialization;

namespace UserStorageServices.Notifications
{
    class DeleteUserActionNotification
    {
        [XmlElement("userId")]
        public int UserId { get; set; }
    }
}
