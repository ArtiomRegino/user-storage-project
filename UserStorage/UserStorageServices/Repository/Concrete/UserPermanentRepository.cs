using System.IO;
using UserStorageServices.IdGenerators.Interfaces;
using UserStorageServices.Repository.Interfaces;
using UserStorageServices.Repository.Serializators;

namespace UserStorageServices.Repository.Concrete
{
    public class UserPermanentRepository : UserTemproraryRepository, IUserRepositoryManager
    {
        private readonly string _filePath;
        private readonly IUserSerializationStrategy _serializer;

        public UserPermanentRepository(IUserSerializationStrategy strategy = null, string filePath = null, IUserIdGenerationService generationService = null) : base(generationService)
        {
            _serializer = strategy ?? new BinaryUserSerializationStrategy();
            _filePath = string.IsNullOrEmpty(filePath) ? "repository.bin" : filePath;
        }

        public void Start()
        {
            using (var fs = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    Users = _serializer.DeserializeUsers(fs);
                    if (Users.Count != 0)
                    {
                        Generator.LastId = Users.FindLast(u => u != null).Id;
                    }
                    
                }
            }
        }

        public void Stop()
        {
            using (var fs = new FileStream(_filePath, FileMode.Create))
            {
                _serializer.SerializeUsers(fs, Users);
            }
        }
    }
}
