using System;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Services
{
    public class UserIdGenerationService : IUserIdGenerationService
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}
