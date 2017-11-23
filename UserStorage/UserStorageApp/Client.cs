using System;
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
            var serviceConfiguration = (ServiceConfigurationSection.ServiceConfigurationSection)ConfigurationManager.GetSection("serviceConfiguration");
            var user = new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            };

            _repository.Start();
            Console.WriteLine("Repository started.");

            _userStorageService.Add(user);
            Console.WriteLine("User added.");
            Console.WriteLine($"Now there are {_userStorageService.Count} users in storage.");

            var users = _userStorageService.SearchByFirstName("Alex");
            Console.WriteLine("Users were found:");
            foreach (var item in users)
            {
                Console.WriteLine($"{item.FirstName} {item.LastName} {item.Age}");
            }

            _userStorageService.Remove(user);
            Console.WriteLine("User removed.");
            Console.WriteLine($"Now there are {_userStorageService.Count} users in storage.");

            _repository.Stop();
            Console.WriteLine("Repository stoped.");
        }
    }
}
