using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Repository;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserMemoryCacheWithStateTests
    {
        [TestMethod]
        public void StartAndStopMethods_NormalLoading_Success()
        {
            // Arrange
            var repository = new UserMemoryCacheWithState();

            File.Delete("repository.bin");

            repository.Start();

            var users = new List<User>();

            for (int i = 0; i < 10; i++)
            {
                users.Add(new User()
                {
                    Age = 20 + i,
                    FirstName = $"User{i}",
                    LastName = $"User{i + 10}"
                });
            }

            foreach (var item in users)
            {
                repository.Set(item);
            }

            repository.Stop();

            repository.Start();

            // Act
            var usersFromRep = repository.Query(u => true).ToList();

            repository.Stop();

            // Assert
            CollectionAssert.AreEqual(usersFromRep, users);
        }
    }
}
