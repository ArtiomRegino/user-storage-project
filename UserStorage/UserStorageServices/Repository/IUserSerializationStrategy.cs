using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Repository
{
    public interface IUserSerializationStrategy
    {
        void SerializeUsers(FileStream fs, List<User> users);
        List<User> DeserializeUsers(FileStream fs);
    }
}
