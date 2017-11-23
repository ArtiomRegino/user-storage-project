using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UserStorageServices.Exceptions;
using UserStorageServices.Validators.Attributes;
using UserStorageServices.Validators.Interfaces;

namespace UserStorageServices.Validators.Concrete
{
    [Serializable]
    public class FirstNameValidator : IValidator
    {
        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var type = typeof(User);
            var propertyInfo = type.GetProperty("FirstName");

            if (propertyInfo != null)
            {
                var attributeNotNullOrEmpty =
                    propertyInfo.GetCustomAttribute(typeof(ValidateNotNullOrEmptyOrWhiteSpaceAttribute));
                var attributeMaxLength = propertyInfo.GetCustomAttribute<ValidateMaxLengthAttribute>();
                var attributeRegex = propertyInfo.GetCustomAttribute<ValidateRegexAttribute>();

                if (attributeNotNullOrEmpty != null)
                {
                    if (string.IsNullOrWhiteSpace(user.FirstName))
                    {
                        throw new FirstNameNullEmptyOrWhitespace("FirstName is null, empty or whitespace.");
                    }
                }

                if (user.FirstName.Length > attributeMaxLength?.Length)
                {
                    throw new FirstNameExceedsLengthLimitsException(nameof(user));
                }

                if (attributeRegex != null)
                {
                    var regex = new Regex(attributeRegex.Pattern);

                    if (!regex.IsMatch(user.FirstName))
                    {
                        throw new FirstNameNotMatchPatternException(nameof(user));
                    }
                }
            }
        }
    }
}
