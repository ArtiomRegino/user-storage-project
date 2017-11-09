using System;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Services
{
    public class UserIdGenerationService : IUserIdGenerationService
    {
        public int LastId { get; set; } = -1;

        public int Generate()
        {
            return ++LastId;
        }
    }
}
