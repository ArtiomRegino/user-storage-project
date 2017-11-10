using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserStorageServices.Repository
{
    public class XmlUserSerializationStrategy : IUserSerializationStrategy
    {
        public void SerializeUsers(FileStream fs, List<User> users)
        {
            var serializer = new XmlSerializer(typeof(List<User>));

            serializer.Serialize(fs, users);
        }

        public List<User> DeserializeUsers(FileStream fs)
        {
            var serializer = new XmlSerializer(typeof(User));

            return (List<User>)serializer.Deserialize(fs);
        }
    }
}
