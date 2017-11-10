using System;
using System.Collections.Generic;
using System.Diagnostics;
using UserStorageServices.Enums;
using UserStorageServices.Services.Interfaces;

namespace UserStorageServices.Services.Concrete
{
    public class UserStorageServiceLog : UserStorageServiceDecorator
    {
        private readonly BooleanSwitch _logging = new BooleanSwitch(
            "enableLogging", "management from app.config");

        public UserStorageServiceLog(IUserStorageService storageService) : base(storageService)
        {
        }

        public override int Count
        {
            get
            {
                if (this._logging.Enabled)
                {
                    Trace.TraceInformation("Count() method is called.");
                }

                return StorageService.Count;
            }
        }

        public override UserStorageServiceMode ServiceMode => StorageService.ServiceMode;

        public override void Add(User user)
        {
            if (this._logging.Enabled)
            {
                Trace.TraceInformation("Add() method is called.");
            }

            StorageService.Add(user);
        }

        public override bool Remove(User user)
        {
            if (this._logging.Enabled)
            {
                Trace.TraceInformation("Remove() method is called.");
            }

            return StorageService.Remove(user);
        }

        public override IEnumerable<User> SearchByAge(int age)
        {
            if (this._logging.Enabled)
            {
                Trace.TraceInformation("SearchByAge() method is called.");
            }

            return StorageService.SearchByAge(age);
        }

        public override IEnumerable<User> SearchByLastName(string lastName)
        {
            if (this._logging.Enabled)
            {
                Trace.TraceInformation("SearchByLastName() method is called.");
            }

            return StorageService.SearchByLastName(lastName);
        }

        public override IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (this._logging.Enabled)
            {
                Trace.TraceInformation("SearchByFirstName() method is called.");
            }

            return StorageService.SearchByFirstName(firstName);
        }

        public override User SearchFirstByPredicate(Predicate<User> predicate)
        {
            if (this._logging.Enabled)
            {
                Trace.TraceInformation("SearchFirstByPredicate() method is called.");
            }

            return StorageService.SearchFirstByPredicate(predicate);
        }

        public override IEnumerable<User> SearchByPredicate(Predicate<User> predicate)
        {
            if (this._logging.Enabled)
            {
                Trace.TraceInformation("SearchByPredicate() method is called.");
            }

            return StorageService.SearchByPredicate(predicate);
        }
    }
}
