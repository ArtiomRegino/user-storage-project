using System;
using UserStorageServices.Exceptions;
using UserStorageServices.Validators.Interfaces;

namespace UserStorageServices.Validators.Concrete
{
    [Serializable]
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
