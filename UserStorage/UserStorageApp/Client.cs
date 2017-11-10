using System.Configuration;
using UserStorageServices;
using UserStorageServices.Repository.Concrete;
using UserStorageServices.Repository.Interfaces;
using UserStorageServices.Repository.Serializators;
using UserStorageServices.Services.Concrete;
using UserStorageServices.Services.Interfaces;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageServiceBase"/>.
    /// </summary>
    public class Client
    {
        private readonly IUserStorageService _userStorageService;
        private readonly IUserRepositoryManager _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService = null, IUserRepositoryManager repository = null)
        {
            var filePath = ConfigurationManager.AppSettings["FilePath"];

            _repository = repository ?? new UserPermanentRepository(new BinaryUserSerializationStrategy(), filePath);
            _userStorageService = userStorageService ?? new UserStorageServiceLog(new UserStorageServiceMaster((IUserRepository)_repository));
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageServiceBase"/> class.
        /// </summary>
        public void Run()
        {
            var user = new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            };

            _repository.Start();

            _userStorageService.Add(user);

            _userStorageService.SearchByFirstName("Alex"); 

            _userStorageService.Remove(user);

            _repository.Stop();
        }
    }
}
