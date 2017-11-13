using System;
using UserStorageServices.Exceptions;
using UserStorageServices.Validators.Interfaces;

namespace UserStorageServices.Validators.Concrete
{
    [Serializable]
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
