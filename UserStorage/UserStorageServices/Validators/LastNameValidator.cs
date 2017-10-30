using System;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    public class LastNameValidator : IValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("LastName is null, empty or whitespace", nameof(user));
            }
        }
    }
}
