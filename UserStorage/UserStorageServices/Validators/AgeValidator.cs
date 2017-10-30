﻿using UserStorageServices.Exceptions;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    public class AgeValidator : IValidator
    {
        public void Validate(User user)
        {
            if (user.Age < 0 || user.Age > 150)
            {
                throw new AgeExceedsLimitsException("Age is out of range (0 - 150).");
            }
        }
    }
}
