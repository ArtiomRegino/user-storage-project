using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            
            // Act
            userStorageService.Add(new User
            {
                FirstName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameConsistsOfWhiteSpaces_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "    "
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameIsEmptyString_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = string.Empty
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserLastNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                LastName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserLastNameConsistsOfWhiteSpaces_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                LastName = "    "
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserLastNameIsEmptyString_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                LastName = string.Empty
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserAgeIsLessThanZero_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                Age = -2
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Add_StandartUser_NothingHappen()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Lorem",
                LastName = "Ipsum",
                Age = 23
            });

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Remove(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_UserNotExist_ReturnedFalse()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act and Assert
            Assert.IsFalse(userStorageService.Remove(new User()));
        }

        [TestMethod]
        public void Remove_CorrectUser_ReturnedTrue()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            var user = new User()
            {
                Age = 21,
                FirstName = "Anna",
                LastName = "Shprot"
            };
            userStorageService.Add(user);

            // Assert 
            Assert.IsTrue(userStorageService.Remove(user));            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByAge_ArgumentLessThanZero_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchByAge(-3);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByAge_NoSuchAUser_Returned()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act and Assert 
            Assert.IsInstanceOfType(userStorageService.SearchByAge(3), typeof(IEnumerable<User>));
        }

        [TestMethod]
        public void SearchByAge_SearchForAnExistingUser_ReturnedIEnumerableWithCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            userStorageService.Add(user);

            // Act and Assert 
            Assert.AreEqual(user, userStorageService.SearchByAge(27).FirstOrDefault());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByLastName_ArgumentIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchByLastName(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByLastName_ArgumentIsWhiteSpace_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchByLastName("   ");

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByLastName_NoSuchAUser_ReturnedEmptyIEnumerable()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            Assert.IsInstanceOfType(userStorageService.SearchByLastName("Freeman"), typeof(IEnumerable<User>));

            // Assert 
        }

        [TestMethod]
        public void SearchByLastName_SearchForAnExistingUser_Returned()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            userStorageService.Add(user);

            // Act
            Assert.AreEqual(user, userStorageService.SearchByLastName("Fresko").FirstOrDefault());

            // Assert 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByFirstName_ArgumentIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchByFirstName(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByFirstName_ArgumentIsWhiteSpace_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchByFirstName("   ");

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByFirstName_NoSuchAUser_ReturnedEmptyIEnumerable()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            Assert.IsInstanceOfType(userStorageService.SearchByFirstName("Morgan"), typeof(IEnumerable<User>));

            // Assert 
        }

        [TestMethod]
        public void SearchByFirstName_SearchForAnExistingUser_ReturnedIEnumerableWithCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            userStorageService.Add(user);

            // Act
            Assert.AreEqual(user, userStorageService.SearchByFirstName("Michael").FirstOrDefault());

            // Assert 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SearchByPredicate_ArgumentIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchByPredicate(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByPredicate_NoSuchAUser_ReturnedEmptyIEnumerable()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            Assert.IsInstanceOfType(userStorageService.SearchByPredicate(u => u.Age == 11), typeof(IEnumerable<User>));

            // Assert 
        }

        [TestMethod]
        public void SearchByPredicate_SearchForAnExistingUser_ReturnedIEnumerableWithCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            userStorageService.Add(user);

            // Act
            Assert.AreEqual(user, userStorageService.SearchByPredicate(u => u.Age == 27).FirstOrDefault());

            // Assert 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SearchFirstByPredicate_ArgumentIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.SearchFirstByPredicate(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchFirstByPredicate_NoSuchAUser_ReturnedNull()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            Assert.IsNull(userStorageService.SearchFirstByPredicate(u => u.Age == 11));

            // Assert 
        }

        [TestMethod]
        public void SearchFirstByPredicate_SearchForAnExistingUser_ReturnedCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            userStorageService.Add(user);

            // Act
            Assert.AreEqual(user, userStorageService.SearchFirstByPredicate(u => u.Age == 27));

            // Assert 
        }
    }
}
