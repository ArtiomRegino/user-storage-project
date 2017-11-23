using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using ServiceConfigurationSection;
using UserStorageServices.Enums;
using UserStorageServices.IdGenerators.Interfaces;
using UserStorageServices.Notifications;
using UserStorageServices.Repository.Concrete;
using UserStorageServices.Repository.Interfaces;
using UserStorageServices.Services.Attributes;
using UserStorageServices.Services.Interfaces;
using UserStorageServices.Validators.Interfaces;

namespace UserStorageServices.Services.Concrete
{
    public static class FactoryService
    {
        private static int _number;

        public static UserStorageServiceSlave CreateSlave(IUserSerializationStrategy strategy = null, string filePath = null, IUserIdGenerationService generationService = null)
        {
            var newDomain = AppDomain.CreateDomain(
                "slaveDomain" + _number++,
                null,
                new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase });

            var repository = newDomain.CreateInstanceAndUnwrap(
                typeof(UserPermanentRepository).Assembly.FullName,
                typeof(UserPermanentRepository).FullName,
                false,
                BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance,
                null,
                new object[] { strategy, filePath, generationService },
                null,
                null) as UserTemproraryRepository;

            return newDomain.CreateInstanceAndUnwrap(
                typeof(UserStorageServiceSlave).Assembly.FullName,
                typeof(UserStorageServiceSlave).FullName,
                false,
                BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance,
                null,
                new object[] { repository },
                null,
                 null) as UserStorageServiceSlave;
        }

        public static UserStorageServiceMaster CreateMaster(IUserSerializationStrategy strategy = null, string filePath = null, IUserIdGenerationService generationService = null, IValidator validator = null)
        {
            var newDomain = AppDomain.CreateDomain(
                "masterDomain",
                null,
                new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase });

            var repositoryForMaster = newDomain.CreateInstanceAndUnwrap(
                typeof(UserPermanentRepository).Assembly.FullName,
                typeof(UserPermanentRepository).FullName,
                false,
                BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance,
                null,
                new object[] { strategy, filePath, generationService },
                null,
                null) as UserTemproraryRepository;

            var master = newDomain.CreateInstanceAndUnwrap(
                typeof(UserStorageServiceMaster).Assembly.FullName,
                typeof(UserStorageServiceMaster).FullName,
                false,
                BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance,
                null,
                new object[] { repositoryForMaster, validator, null },
                null,
                null) as UserStorageServiceMaster;

            return master;
        }

        public static IUserStorageService CreateService(
            string serviceType,
            IUserSerializationStrategy strategy = null,
            string filePath = null,
            IUserIdGenerationService generationService = null,
            IValidator validator = null)
        {
            var types = new List<Type>();

            foreach (var item in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (item.GetCustomAttributes(typeof(MyApplicationServiceAttribute), true).Length > 0)
                {
                    types.Add(item);
                } 
            }

            var type = types.FirstOrDefault(t => t.Name == serviceType);

            if (type == null)
            {
                throw new NullReferenceException("There's no such a type in asscembly.");
            }

            var newDomain = AppDomain.CreateDomain(
                serviceType + _number++,
                null,
                new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase });

            var repository = newDomain.CreateInstanceAndUnwrap(
                typeof(UserPermanentRepository).Assembly.FullName,
                typeof(UserPermanentRepository).FullName,
                false,
                BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance,
                null,
                new object[] { strategy, filePath, generationService },
                null,
                null) as UserTemproraryRepository;

            return Activator.CreateInstance(
                newDomain,
                type.Assembly.FullName,
                type.FullName,
                false,
                BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance,
                null,
                new object[] { repository },
                null,
                null).Unwrap() as IUserStorageService;
        }

        public static UserStorageServiceMaster DefaultCreation(
            ServiceConfigurationSection.ServiceConfigurationSection configurationSection,
            IUserSerializationStrategy strategy = null,
            string filePath = null,
            IUserIdGenerationService generationService = null,
            IValidator validator = null)
        {
            if (configurationSection.ServiceInstances.Count(i => i.Mode == ServiceInstanceMode.Master) != 1)
            {
                throw new ConfigurationErrorsException("It should be one MasterService.");
            }

            UserStorageServiceMaster masterService = null;
            var slaveServices = new List<IUserStorageService>();

            foreach (var instance in configurationSection.ServiceInstances)
            {
                var localService = CreateService(instance.Type);

                if (localService.ServiceMode == UserStorageServiceMode.MasterNode)
                {
                    masterService = (UserStorageServiceMaster)localService;
                }
                else
                {
                    slaveServices.Add(localService);
                }
            }

            foreach (var item in slaveServices)
            {
                masterService.AddSubscriber((INotificationSubscriber)item);
            }
            
            return masterService;
        }
    }
}
