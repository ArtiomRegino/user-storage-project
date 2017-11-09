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
        public void StartAndStopMethods_NormalLoadingWithBinaryUserSerializationStrategy_Success()
        {
            // Arrange
            var repository = new UserPermanentRepository(new BinaryUserSerializationStrategy(), "binRepository.bin");

            if (File.Exists("binRepository.bin"))
            {
                File.Delete("binRepository.bin");
            }
            

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

        [TestMethod]
        public void StartAndStopMethods_NormalLoadingWithXmlUserSerializationStrategy_Success()
        {
            // Arrange
            var repository = new UserPermanentRepository(new XmlUserSerializationStrategy(), "xmlRepository.xml");

            if (File.Exists("xmlRepository.xml"))
            {
                File.Delete("xmlRepository.xml");
            }

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
