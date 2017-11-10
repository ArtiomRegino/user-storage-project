using System.IO;
using UserStorageServices.IdGenerators.Interfaces;
using UserStorageServices.Repository.Interfaces;

namespace UserStorageServices.Repository.Concrete
{
    public class UserPermanentRepository : UserTemproraryRepository, IUserRepositoryManager
    {
        private readonly string _filePath;
        private readonly IUserSerializationStrategy _serializer;

        public UserPermanentRepository(IUserSerializationStrategy strategy, string filePath = null, IUserIdGenerationService generationService = null) : base(generationService)
        {
            _serializer = strategy;
            _filePath = string.IsNullOrEmpty(filePath) ? "repository.bin" : filePath;
        }

        public void Start()
        {
            using (var fs = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    Users = _serializer.DeserializeUsers(fs);
                    Generator.LastId = Users.FindLast(u => u != null).Id;
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
