using System.Configuration;
using UserStorageServices;
using UserStorageServices.Interfaces;
using UserStorageServices.Repository;
using UserStorageServices.Services;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageServiceBase"/>.
    /// </summary>
    public class Client
    {
        private readonly IUserStorageService _userStorageService;
        private readonly IUserRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService = null, IUserRepository repository = null)
        {
            var filePath = ConfigurationManager.AppSettings["FilePath"];

            _repository = repository ?? new UserMemoryCacheWithState(filePath);
            _userStorageService = userStorageService ?? new UserStorageServiceLog(new UserStorageServiceMaster(_repository));
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

            _userStorageService.Remove(user);

            _userStorageService.SearchByFirstName("Alex");

            _repository.Stop();
        }
    }
}
