namespace UserStorageServices.IdGenerators.Interfaces
{
    public interface IUserIdGenerationService
    {
        int LastId { get; set; }

        int Generate();
    }
}
