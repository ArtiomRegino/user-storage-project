using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Repository
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        private readonly string _filePath;
        private readonly IUserSerializationStrategy _serializer;

        public UserMemoryCacheWithState(IUserSerializationStrategy strategy, string filePath = null, IUserIdGenerationService generationService = null) : base(generationService)
        {
            _serializer = strategy;
            _filePath = string.IsNullOrEmpty(filePath) ? "repository.bin" : filePath;
        }

        public override void Start()
        {
            using (var fs = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    _users = _serializer.DeserializeUsers(fs);
                }
            }
        }

        public override void Stop()
        {
            using (var fs = new FileStream("repository.bin", FileMode.Create))
            {
                _serializer.SerializeUsers(fs, _users);
            }
        }
    }
}
