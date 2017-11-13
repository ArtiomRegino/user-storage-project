using System;
using UserStorageServices.Exceptions;
using UserStorageServices.Validators.Interfaces;

namespace UserStorageServices.Validators.Concrete
{
    [Serializable]
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
