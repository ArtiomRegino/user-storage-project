using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorageServices.Repository.Interfaces;

namespace UserStorageServices.Repository.Serializators
{
    [Serializable]
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
