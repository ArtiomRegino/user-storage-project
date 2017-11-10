using System;
using UserStorageServices.Validators.Interfaces;

namespace UserStorageServices.Validators.Concrete
{
    public class CompositeValidator : IValidator
    {
        private readonly IValidator[] _validators;

        public CompositeValidator()
        {
            _validators = new[]
            {
                new AgeValidator(),
                new FirstNameValidator(),
                (IValidator)new LastNameValidator()
            };
        }

        public void Validate(User user)
        {
            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException(nameof(user));
            }

            foreach (var item in _validators)
            {
                item.Validate(user);
            }
        }
    }
}
