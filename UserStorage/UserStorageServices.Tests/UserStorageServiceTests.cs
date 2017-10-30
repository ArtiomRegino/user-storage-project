﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Exceptions;
using UserStorageServices.Interfaces;
using UserStorageServices.Services;
using UserStorageServices.Validators;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        private UserIdGenerationService userIdGenerationService = new UserIdGenerationService();
        private IValidator _validator = new CompositeValidator();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameNullEmptyOrWhitespace))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.Add(new User
            {
                FirstName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameNullEmptyOrWhitespace))]
        public void Add_UserFirstNameConsistsOfWhiteSpaces_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.Add(new User
            {
                FirstName = "    "
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameNullEmptyOrWhitespace))]
        public void Add_UserFirstNameIsEmptyString_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.Add(new User
            {
                FirstName = string.Empty
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameNullEmptyOrWhitespace))]
        public void Add_UserLastNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.Add(new User
            {
                FirstName = "Alan",
                LastName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameNullEmptyOrWhitespace))]
        public void Add_UserLastNameConsistsOfWhiteSpaces_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.Add(new User
            {
                FirstName = "Alan",
                LastName = "    "
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameNullEmptyOrWhitespace))]
        public void Add_UserLastNameIsEmptyString_ExceptionThrown()
        {
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.Add(new User
            {
                FirstName = "Alan",
                LastName = string.Empty
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(AgeExceedsLimitsException))]
        public void Add_UserAgeIsLessThanZero_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.Add(new User
            {
                Age = -2
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Add_StandartUser_NothingHappen()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.Add(new User
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
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.Remove(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_UserNotExist_ReturnedFalse()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act and Assert
            Assert.IsFalse(storageLog.Remove(new User()));
        }

        [TestMethod]
        public void Remove_CorrectUser_ReturnedTrue()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            var user = new User()
            {
                Age = 21,
                FirstName = "Anna",
                LastName = "Shprot"
            };
            storageLog.Add(user);

            // Assert 
            Assert.IsTrue(userStorageService.Remove(user));            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByAge_ArgumentLessThanZero_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.SearchByAge(-3);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByAge_NoSuchAUser_Returned()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act and Assert 
            Assert.IsInstanceOfType(storageLog.SearchByAge(3), typeof(IEnumerable<User>));
        }

        [TestMethod]
        public void SearchByAge_SearchForAnExistingUser_ReturnedIEnumerableWithCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            storageLog.Add(user);

            // Act and Assert 
            Assert.AreEqual(user, storageLog.SearchByAge(27).FirstOrDefault());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByLastName_ArgumentIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.SearchByLastName(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByLastName_ArgumentIsWhiteSpace_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.SearchByLastName("   ");

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByLastName_NoSuchAUser_ReturnedEmptyIEnumerable()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            Assert.IsInstanceOfType(storageLog.SearchByLastName("Freeman"), typeof(IEnumerable<User>));

            // Assert 
        }

        [TestMethod]
        public void SearchByLastName_SearchForAnExistingUser_Returned()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            storageLog.Add(user);

            // Act
            Assert.AreEqual(user, storageLog.SearchByLastName("Fresko").FirstOrDefault());

            // Assert 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByFirstName_ArgumentIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.SearchByFirstName(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByFirstName_ArgumentIsWhiteSpace_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.SearchByFirstName("   ");

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByFirstName_NoSuchAUser_ReturnedEmptyIEnumerable()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            Assert.IsInstanceOfType(storageLog.SearchByFirstName("Morgan"), typeof(IEnumerable<User>));

            // Assert 
        }

        [TestMethod]
        public void SearchByFirstName_SearchForAnExistingUser_ReturnedIEnumerableWithCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            storageLog.Add(user);

            // Act
            Assert.AreEqual(user, storageLog.SearchByFirstName("Michael").FirstOrDefault());

            // Assert 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SearchByPredicate_ArgumentIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.SearchByPredicate(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByPredicate_NoSuchAUser_ReturnedEmptyIEnumerable()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            Assert.IsInstanceOfType(storageLog.SearchByPredicate(u => u.Age == 11), typeof(IEnumerable<User>));

            // Assert 
        }

        [TestMethod]
        public void SearchByPredicate_SearchForAnExistingUser_ReturnedIEnumerableWithCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            storageLog.Add(user);

            // Act
            Assert.AreEqual(user, storageLog.SearchByPredicate(u => u.Age == 27).FirstOrDefault());

            // Assert 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SearchFirstByPredicate_ArgumentIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            storageLog.SearchFirstByPredicate(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchFirstByPredicate_NoSuchAUser_ReturnedNull()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            Assert.IsNull(storageLog.SearchFirstByPredicate(u => u.Age == 11));

            // Assert 
        }

        [TestMethod]
        public void SearchFirstByPredicate_SearchForAnExistingUser_ReturnedCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            storageLog.Add(user);

            // Act
            Assert.AreEqual(user, storageLog.SearchFirstByPredicate(u => u.Age == 27));

            // Assert 
        }

        [TestMethod]
        public void SearchFirstByPredicate_SearchByFirstNameAndLastNameForAnExistingUser_ReturnedCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            storageLog.Add(user);
            var userAfterSearch  =
                storageLog.SearchFirstByPredicate(u => u.FirstName == "Michael" && u.LastName == "Fresko");
           
            // Act
            Assert.AreEqual(user, userAfterSearch);

            // Assert 
        }

        [TestMethod]
        public void SearchFirstByPredicate_SearchByFirstNameAndAgeForAnExistingUser_ReturnedCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            storageLog.Add(user);
            var userAfterSearch =
                storageLog.SearchFirstByPredicate(u => u.FirstName == "Michael" && u.Age == 27);
           
            // Act
            Assert.AreEqual(user, userAfterSearch);

            // Assert 
        }

        [TestMethod]
        public void SearchFirstByPredicate_SearchByLastNameAndAgeForAnExistingUser_ReturnedCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            storageLog.Add(user);
            var userAfterSearch =
                storageLog.SearchFirstByPredicate(u => u.LastName == "Fresko" && u.Age == 27);
            
            // Act
            Assert.AreEqual(user, userAfterSearch);

            // Assert 
        }

        [TestMethod]
        public void SearchFirstByPredicate_SearchByFirstNameLastNameAndAgeForAnExistingUser_ReturnedCurrentUser()
        {
            // Arrange
            var userStorageService = new UserStorageService(userIdGenerationService, _validator);
            var storageLog = new UserStorageServiceLog(userStorageService);
            var user = new User()
            {
                Age = 27,
                FirstName = "Michael",
                LastName = "Fresko"
            };
            storageLog.Add(user);
            var userAfterSearch =
                storageLog.SearchFirstByPredicate(u => u.LastName == "Fresko" && u.Age == 27 && u.FirstName == "Michael");
           
            // Act
            Assert.AreEqual(user, userAfterSearch);

            // Assert 
        }
    }
}
