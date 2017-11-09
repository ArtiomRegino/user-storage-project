﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Repository
{
    public class UserPermanentRepository : UserTemproraryRepository
    {
        private readonly string _filePath;
        private readonly IUserSerializationStrategy _serializer;

        public UserPermanentRepository(IUserSerializationStrategy strategy, string filePath = null, IUserIdGenerationService generationService = null) : base(generationService)
        {
            _serializer = strategy;
            _filePath = string.IsNullOrEmpty(filePath) ? "repository.bin" : filePath;
        }

        public override void Start()
        {
            using (var fs = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    _users = _serializer.DeserializeUsers(fs);
                    _generator.LastId = _users.FindLast(u => u != null).Id;
                }
            }
        }

        public override void Stop()
        {
            using (var fs = new FileStream(_filePath, FileMode.Create))
            {
                _serializer.SerializeUsers(fs, _users);
            }
        }
    }
}