using System;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    public class AgeValidator : IValidator
    {
        public void Validate(User user)
        {
            if (user.Age < 0 || user.Age > 150)
            {
                throw new ArgumentException("Age is out of range (0 - 150).", nameof(user));
            }
        }
    }
}
