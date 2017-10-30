using System;
using UserStorageServices.Exceptions;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    public class LastNameValidator : IValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new LastNameNullEmptyOrWhitespace("LastName is null, empty or white-space");
            }
        }
    }
}
