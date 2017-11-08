using UserStorageServices.Interfaces;

namespace UserStorageServices.Repository
{
    class UserMemoryCacheWithState : UserMemoryCache
    {
        public UserMemoryCacheWithState(IUserIdGenerationService generationService) : base(generationService)
        {
        }

    }
}
