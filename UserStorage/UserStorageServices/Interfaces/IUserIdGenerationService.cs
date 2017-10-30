using System;

namespace UserStorageServices.Interfaces
{
    public interface IUserIdGenerationService
    {
        Guid Generate();
    }
}
