using System;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    public class FirstNameValidator : IValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("FirstName is null, empty or whitespace", nameof(user));
            }
        }
    }
}
