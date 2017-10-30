﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Services
{
    public class UserStorageServiceLog : UserStorageServiceDecorator
    {
        public UserStorageServiceLog(IUserStorageService storageService) : base(storageService)
        {
        }

        private readonly BooleanSwitch _logging = new BooleanSwitch(
            "enableLogging", "management from app.config");

        public override int Count
        {
            get
            {
                if (_logging.Enabled)
                {
                    Console.WriteLine("Count() method is called.");
                }

                return _storageService.Count;
            }
        }

        public override void Add(User user)
        {
            if (_logging.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            _storageService.Add(user);
        }

        public override bool Remove(User user)
        {
            if (_logging.Enabled)
            {
                Console.WriteLine("Remove() method is called.");
            }

            return _storageService.Remove(user);
        }

        public override IEnumerable<User> SearchByAge(int age)
        {
            if (_logging.Enabled)
            {
                Console.WriteLine("SearchByAge() method is called.");
            }

            return _storageService.SearchByAge(age);
        }

        public override IEnumerable<User> SearchByLastName(string lastName)
        {
            if (_logging.Enabled)
            {
                Console.WriteLine("SearchByLastName() method is called.");
            }

            return _storageService.SearchByLastName(lastName);
        }

        public override IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (_logging.Enabled)
            {
                Console.WriteLine("SearchByFirstName() method is called.");
            }

            return _storageService.SearchByFirstName(firstName);
        }

        public override User SearchFirstByPredicate(Predicate<User> predicate)
        {
            if (_logging.Enabled)
            {
                Console.WriteLine("SearchFirstByPredicate() method is called.");
            }

            return _storageService.SearchFirstByPredicate(predicate);
        }

        public override IEnumerable<User> SearchByPredicate(Predicate<User> predicate)
        {
            if (_logging.Enabled)
            {
                Console.WriteLine("SearchByPredicate() method is called.");
            }

            return _storageService.SearchByPredicate(predicate);
        }

        
    }
}
