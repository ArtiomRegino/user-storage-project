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
            this._serializer = strategy;
            this._filePath = string.IsNullOrEmpty(filePath) ? "repository.bin" : filePath;
        }

        public override void Start()
        {
            using (var fs = new FileStream(this._filePath, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    this.Users = this._serializer.DeserializeUsers(fs);
                    this.Generator.LastId = this.Users.FindLast(u => u != null).Id;
                }
            }
        }

        public override void Stop()
        {
            using (var fs = new FileStream(this._filePath, FileMode.Create))
            {
                this._serializer.SerializeUsers(fs, this.Users);
            }
        }
    }
}
