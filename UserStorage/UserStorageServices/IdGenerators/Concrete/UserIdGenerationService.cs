using System;
using UserStorageServices.IdGenerators.Interfaces;

namespace UserStorageServices.IdGenerators.Concrete
{
    [Serializable]
    public class UserIdGenerationService : IUserIdGenerationService
    {
        public int LastId { get; set; } = -1;

        public int Generate()
        {
            return ++LastId;
        }
    }
}
