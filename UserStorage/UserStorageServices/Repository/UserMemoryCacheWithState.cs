using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Repository
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        public UserMemoryCacheWithState(IUserIdGenerationService generationService = null) : base(generationService)
        {
        }

        public override void Start()
        {
            using (var fs = new FileStream("repository.bin", FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                if (fs.Length != 0)
                {
                    _users = (List<User>) formatter.Deserialize(fs);
                }
            }
        }

        public override void Stop()
        {
            using (var fs = new FileStream("repository.bin", FileMode.Create))
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(fs, _users);
            }
        }
    }
}
