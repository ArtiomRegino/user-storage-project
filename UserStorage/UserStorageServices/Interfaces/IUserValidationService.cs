﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Interfaces
{
    public interface IValidator
    {
        void Validate(User user);
    }
}
