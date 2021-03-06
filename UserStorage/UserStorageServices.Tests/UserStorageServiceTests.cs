﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorageServices.Exceptions;
using UserStorageServices.IdGenerators.Concrete;
using UserStorageServices.Repository.Concrete;
using UserStorageServices.Services.Concrete;
using UserStorageServices.Services.Interfaces;
using UserStorageServices.Validators.Concrete;
using UserStorageServices.Validators.Interfaces;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        private UserIdGenerationService _idGenerator = new UserIdGenerationService();
        private IValidator _validator = new CompositeValidator();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAsUserArgumentWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameNullEmptyOrWhitespace))]
        public void Add_UserFirstNameIsNullWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                FirstName = null,
                Age = 18,
                LastName = "Mariancol"
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameNullEmptyOrWhitespace))]
        public void Add_UserFirstNameConsistsOfWhiteSpacesWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                FirstName = "    ",
                Age = 28,
                LastName = "Regino"
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameNullEmptyOrWhitespace))]
        public void Add_UserFirstNameIsEmptyStringWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                FirstName = string.Empty,
                Age = 24,
                LastName = "Bolinik"
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameExceedsLengthLimitsException))]
        public void Add_UserFirstNameLengthIsMoreThenCanBeWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                FirstName = "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD",
                Age = 24,
                LastName = "Bolinik"
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(FirstNameNotMatchPatternException))]
        public void Add_UserFirstNameRegexConditionFailWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                FirstName = "Алан/",
                Age = 24,
                LastName = "Bolinik"
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameNotMatchPatternException))]
        public void Add_UserLastNameRegexConditionFailWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                FirstName = "Alan",
                Age = 24,
                LastName = "Алан?!"
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameExceedsLengthLimitsException))]
        public void Add_UserLastNameLengthIsMoreThenCanBeWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                LastName = "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD",
                Age = 24,
                FirstName = "Bolinik"
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameNullEmptyOrWhitespace))]
        public void Add_UserLastNameIsNullWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                FirstName = "Alan",
                LastName = null,
                Age = 22
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameNullEmptyOrWhitespace))]
        public void Add_UserLastNameConsistsOfWhiteSpacesWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                FirstName = "Alan",
                LastName = "    ",
                Age = 34
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(LastNameNullEmptyOrWhitespace))]
        public void Add_UserLastNameIsEmptyStringWithoutSlaveNodes_ExceptionThrown()
        {
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                FirstName = "Alan",
                LastName = string.Empty,
                Age = 32
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(AgeExceedsLimitsException))]
        public void Add_UserAgeIsLessThanZeroWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Add(new User
            {
                FirstName = "Tomara",
                LastName = "Soboleva",
                Age = -2
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Add_StandartUserWithoutSlaveNodes_NothingHappen()
        {
            // Arrange
            var storageLog = GetServiceLog();

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
        public void Remove_NullAsUserArgumentWithoutSlaveNodes_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.Remove(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_UserNotExistWithoutSlaveNodes_ReturnedFalse()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act and Assert
            Assert.IsFalse(storageLog.Remove(new User { Id = 23 }));
        }

        [TestMethod]
        public void Remove_CorrectUserWithoutSlaveNodes_ReturnedTrue()
        {
            // Arrange
            var userStorageService = new UserStorageServiceMaster(new UserTemproraryRepository(_idGenerator));
            var storageLog = new UserStorageServiceLog(userStorageService);

            // Act
            var user = new User
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
            var storageLog = GetServiceLog();

            // Act
            storageLog.SearchByAge(-3);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByAge_NoSuchAUser_Returned()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act and Assert 
            Assert.IsInstanceOfType(storageLog.SearchByAge(3), typeof(IEnumerable<User>));
        }

        [TestMethod]
        public void SearchByAge_SearchForAnExistingUser_ReturnedIEnumerableWithCurrentUser()
        {
            // Arrange
            var storageLog = GetServiceLog();
            var user = new User
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
            var storageLog = GetServiceLog();

            // Act
            storageLog.SearchByLastName(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByLastName_ArgumentIsWhiteSpace_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.SearchByLastName("   ");

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByLastName_NoSuchAUser_ReturnedEmptyIEnumerable()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            Assert.IsInstanceOfType(storageLog.SearchByLastName("Freeman"), typeof(IEnumerable<User>));

            // Assert 
        }

        [TestMethod]
        public void SearchByLastName_SearchForAnExistingUser_Returned()
        {
            // Arrange
            var storageLog = GetServiceLog();
            var user = new User
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
            var storageLog = GetServiceLog();

            // Act
            storageLog.SearchByFirstName(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByFirstName_ArgumentIsWhiteSpace_ExceptionThrown()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            storageLog.SearchByFirstName("   ");

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByFirstName_NoSuchAUser_ReturnedEmptyIEnumerable()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            Assert.IsInstanceOfType(storageLog.SearchByFirstName("Morgan"), typeof(IEnumerable<User>));

            // Assert 
        }

        [TestMethod]
        public void SearchByFirstName_SearchForAnExistingUser_ReturnedIEnumerableWithCurrentUser()
        {
            // Arrange
            var storageLog = GetServiceLog();
            var user = new User
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
            var storageLog = GetServiceLog();

            // Act
            storageLog.SearchByPredicate(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByPredicate_NoSuchAUser_ReturnedEmptyIEnumerable()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            Assert.IsInstanceOfType(storageLog.SearchByPredicate(u => u.Age == 11), typeof(IEnumerable<User>));

            // Assert 
        }

        [TestMethod]
        public void SearchByPredicate_SearchForAnExistingUser_ReturnedIEnumerableWithCurrentUser()
        {
            // Arrange
            var storageLog = GetServiceLog();
            var user = new User
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
            var storageLog = GetServiceLog();

            // Act
            storageLog.SearchFirstByPredicate(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchFirstByPredicate_NoSuchAUser_ReturnedNull()
        {
            // Arrange
            var storageLog = GetServiceLog();

            // Act
            Assert.IsNull(storageLog.SearchFirstByPredicate(u => u.Age == 11));

            // Assert 
        }

        [TestMethod]
        public void SearchFirstByPredicate_SearchForAnExistingUser_ReturnedCurrentUser()
        {
            // Arrange
            var storageLog = GetServiceLog();
            var user = new User
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
            var storageLog = GetServiceLog();
            var user = new User
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
            var storageLog = GetServiceLog();
            var user = new User
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
            var storageLog = GetServiceLog();
            var user = new User
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
            var storageLog = GetServiceLog();
            var user = new User
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

        [TestMethod]
        public void Remove_UserNotExistAndUserExistsServiceWithSlaveNodes_ReturnedTrue()
        {
            // Arrange
            var user1 = new User
            {   
                Id = 23,
                LastName = "Dani",
                FirstName = "Kar",
                Age = 23
            };

            var user2 = new User
            {
                LastName = "Danialis",
                FirstName = "Karl",
                Age = 23
            };

            var slaveService = new UserStorageServiceSlave(new UserTemproraryRepository(_idGenerator));

            var slaveServiceCollection = new List<IUserStorageService> { slaveService };

            var userStorageService = new UserStorageServiceMaster(new UserTemproraryRepository(_idGenerator), _validator, slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(userStorageService);

            storageLog.Add(user2);
            var user = storageLog.SearchFirstByPredicate(u => u.LastName == "Danialis" && u.FirstName == "Karl" && u.Age == 23);

            storageLog.Remove(user);
            storageLog.Remove(user1);

            ////Assert
            Assert.IsTrue(slaveService.Count == 0);
        }

        [TestMethod]
        public void Add_AddingUserServiceWithSlaveNodes_ReturnedTrue()
        {
            // Arrange
            var user1 = new User
            {
                LastName = "Danialis",
                FirstName = "Karl",
                Age = 23
            };

            var slaveService = new UserStorageServiceSlave(new UserTemproraryRepository(_idGenerator));

            var slaveServiceCollection = new List<IUserStorageService> { slaveService };

            var userStorageService = new UserStorageServiceMaster(new UserTemproraryRepository(_idGenerator), _validator, slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(userStorageService);

            storageLog.Add(user1);

            var user = storageLog.SearchFirstByPredicate(u => u.LastName == "Danialis" && u.FirstName == "Karl" && u.Age == 23);
            var userSlave = slaveService.SearchFirstByPredicate(u => u.LastName == "Danialis" && u.FirstName == "Karl" && u.Age == 23);

            bool result = userSlave.FirstName == user.FirstName && userSlave.LastName == user.LastName && userSlave.Age == user.Age;

            ////Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add_AddingUserServiceWithSubscribers_ReturnedTrue()
        {
            // Arrange
            var user1 = new User
            {
                LastName = "Danialis",
                FirstName = "Karl",
                Age = 23
            };

            var subscriber = new UserStorageServiceSlave(new UserTemproraryRepository(_idGenerator));

            var userStorageService = new UserStorageServiceMaster(new UserTemproraryRepository(_idGenerator), _validator);

            userStorageService.AddSubscriber(subscriber);

            var storageLog = new UserStorageServiceLog(userStorageService);

            storageLog.Add(user1);

            var user = storageLog.SearchFirstByPredicate(u => u.LastName == "Danialis" && u.FirstName == "Karl" && u.Age == 23);
            var userSlave = subscriber.SearchFirstByPredicate(u => u.LastName == "Danialis" && u.FirstName == "Karl" && u.Age == 23);

            bool result = userSlave.FirstName == user.FirstName && userSlave.LastName == user.LastName && userSlave.Age == user.Age;

            ////Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Remove_UserNotExistAndUserExistsServiceWithSubscribers_ReturnedTrue()
        {
            // Arrange
            var user1 = new User
            {
                Id = 23,
                LastName = "Dani",
                FirstName = "Kar",
                Age = 23
            };

            var user2 = new User
            {
                LastName = "Danialis",
                FirstName = "Karl",
                Age = 23
            };

            var subscriber = new UserStorageServiceSlave(new UserTemproraryRepository(_idGenerator));

            var userStorageService = new UserStorageServiceMaster(new UserTemproraryRepository(_idGenerator), _validator);

            userStorageService.AddSubscriber(subscriber);

            var storageLog = new UserStorageServiceLog(userStorageService);

            storageLog.Add(user2);
            var user = storageLog.SearchFirstByPredicate(u => u.LastName == "Danialis" && u.FirstName == "Karl" && u.Age == 23);

            storageLog.Remove(user);
            storageLog.Remove(user1);

            ////Assert
            Assert.IsTrue(subscriber.Count == 0);
        }

        [TestMethod]
        public void Add_WithNotifications_ReturnedTrue()
        {
            // Arrange
            var user1 = new User
            {
                Id = 23,
                LastName = "Dani",
                FirstName = "Kar",
                Age = 23
            };

            var user2 = new User
            {
                LastName = "Danialis",
                FirstName = "Karl",
                Age = 23
            };

            var slave = new UserStorageServiceSlave(new UserTemproraryRepository());

            var master = new UserStorageServiceMaster(new UserTemproraryRepository());

            master.Sender.AddReceiver(slave.Receiver);

            master.Add(user1);
            master.Add(user2);

            ////Assert
            Assert.IsTrue(master.Count == slave.Count);
        }

        [TestMethod]
        public void Remove_WithNotifications_ReturnedTrue()
        {
            // Arrange
            var user1 = new User
            {
                Id = 23,
                LastName = "Dani",
                FirstName = "Kar",
                Age = 23
            };

            var user2 = new User
            {
                LastName = "Danialis",
                FirstName = "Karl",
                Age = 23
            };

            var slave = new UserStorageServiceSlave(new UserTemproraryRepository());

            var master = new UserStorageServiceMaster(new UserTemproraryRepository());

            master.Sender.AddReceiver(slave.Receiver);

            master.Add(user1);
            master.Add(user2);

            master.Remove(user1);
            master.Remove(user2);

            ////Assert
            Assert.IsTrue(master.Count == slave.Count && master.Count == 0);
        }

        [TestMethod]
        public void Add_WithNotificationsSomeSenders_ReturnedTrue()
        {
            // Arrange
            var user1 = new User
            {
                Id = 23,
                LastName = "Dani",
                FirstName = "Kar",
                Age = 23
            };

            var user2 = new User
            {
                LastName = "Danialis",
                FirstName = "Karl",
                Age = 23
            };

            var slaveA = new UserStorageServiceSlave(new UserTemproraryRepository());
            var slaveB = new UserStorageServiceSlave(new UserTemproraryRepository());

            var master = new UserStorageServiceMaster(new UserTemproraryRepository());

            master.Sender.AddReceiver(slaveA.Receiver);
            master.Sender.AddReceiver(slaveB.Receiver);

            master.Add(user1);
            master.Add(user2);

            ////Assert
            Assert.IsTrue(master.Count == slaveB.Count && master.Count == slaveA.Count);
        }

        [TestMethod]
        public void Remove_WithNotificationsSomeSenders_ReturnedTrue()
        {
            // Arrange
            var user1 = new User
            {
                Id = 23,
                LastName = "Dani",
                FirstName = "Kar",
                Age = 23
            };

            var user2 = new User
            {
                LastName = "Danialis",
                FirstName = "Karl",
                Age = 23
            };

            var slaveA = new UserStorageServiceSlave(new UserTemproraryRepository());
            var slaveB = new UserStorageServiceSlave(new UserTemproraryRepository());

            var master = new UserStorageServiceMaster(new UserTemproraryRepository());

            master.Sender.AddReceiver(slaveA.Receiver);
            master.Sender.AddReceiver(slaveB.Receiver);

            master.Add(user1);
            master.Add(user2);

            master.Remove(user1);
            master.Remove(user2);

            ////Assert
            Assert.IsTrue(master.Count == slaveA.Count && master.Count == 0 && master.Count == slaveB.Count);
        }

        private UserStorageServiceLog GetServiceLog()
        {
            var userStorageService = new UserStorageServiceMaster(new UserTemproraryRepository(_idGenerator), _validator);
            return new UserStorageServiceLog(userStorageService);
        }
    }
}
