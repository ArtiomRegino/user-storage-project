namespace UserStorageServices.Repository
{
    interface IUserRepository
    {
        void Start();

        void Stop();

        void Get();

        void Set();

        void Query();
    }
}
