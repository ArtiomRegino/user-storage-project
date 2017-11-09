using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Repository
{
    public class BinaryUserSerializationStrategy : IUserSerializationStrategy
    {
        public void SerializeUsers(FileStream fs, List<User> users)
        {
            var formatter = new BinaryFormatter();

            formatter.Serialize(fs, users);
        }

        public List<User> DeserializeUsers(FileStream fs)
        {
            var formatter = new BinaryFormatter();

            return (List<User>)formatter.Deserialize(fs);
        }
    }
}
