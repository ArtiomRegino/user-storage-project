using System.Collections.Generic;
using System.IO;

namespace UserStorageServices.Repository.Interfaces
{
    public interface IUserSerializationStrategy
    {
        void SerializeUsers(FileStream fs, List<User> users);

        List<User> DeserializeUsers(FileStream fs);
    }
}
