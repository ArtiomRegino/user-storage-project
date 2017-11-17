using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using ServiceConfigurationSection;
using UserStorageServices.IdGenerators.Interfaces;
using UserStorageServices.Repository.Concrete;
using UserStorageServices.Repository.Interfaces;
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

            var repositoryA = newDomain.CreateInstanceAndUnwrap(
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
                new object[] { repositoryA },
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

            var master = CreateMaster();

            foreach (var item in configurationSection.ServiceInstances)
            {
                if (item.Mode == ServiceInstanceMode.Slave)
                {
                    master.Sender.AddReceiver(CreateSlave().Receiver);
                }
            }
            
            return master;
        }
    }
}
