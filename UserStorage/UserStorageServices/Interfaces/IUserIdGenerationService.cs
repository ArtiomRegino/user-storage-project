using System;

namespace UserStorageServices.Interfaces
{
    public interface IUserIdGenerationService
    {
        int LastId { get; set; }

        int Generate();
    }
}
