using System;
using UserStorageServices.Exceptions;
using UserStorageServices.Validators.Attributes;
using UserStorageServices.Validators.Interfaces;

namespace UserStorageServices.Validators.Concrete
{
    [Serializable]
    public class AgeValidator : IValidator
    {
        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var type = typeof(User);
            var propertyInfo = type.GetProperty("Age");

            if (propertyInfo == null) return;

            var attributes = propertyInfo.GetCustomAttributes(true);

            foreach (var item in attributes)
            {
                var attribute = item as ValidateMinMaxAttribute;

                if (attribute != null)
                {
                    var max = attribute.Max;
                    var min = attribute.Min;

                    if (user.Age < min || user.Age > max)
                    {
                        throw new AgeExceedsLimitsException($"Age is out of range ({min} - {max}).");
                    }
                }
            }
        }
    }
}
