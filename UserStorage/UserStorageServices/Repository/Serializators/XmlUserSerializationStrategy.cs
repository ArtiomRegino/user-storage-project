using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UserStorageServices.Repository.Interfaces;

namespace UserStorageServices.Repository.Serializators
{
    [Serializable]
    public class XmlUserSerializationStrategy : IUserSerializationStrategy
    {
        public void SerializeUsers(FileStream fs, List<User> users)
        {
            var serializer = new XmlSerializer(typeof(List<User>));

            serializer.Serialize(fs, users);
        }

        public List<User> DeserializeUsers(FileStream fs)
        {
            var serializer = new XmlSerializer(typeof(List<User>));

            return (List<User>)serializer.Deserialize(fs);
        }
    }
}
