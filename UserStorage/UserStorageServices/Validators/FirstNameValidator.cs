using System;
using UserStorageServices.Exceptions;
using UserStorageServices.Interfaces;

namespace UserStorageServices.Validators
{
    public class FirstNameValidator : IValidator
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new FirstNameNullEmptyOrWhitespace("FirstName is null, empty or whitespace");
            }
        }
    }
}
