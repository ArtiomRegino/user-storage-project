using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UserStorageServices.Exceptions;
using UserStorageServices.Validators.Attributes;
using UserStorageServices.Validators.Interfaces;

namespace UserStorageServices.Validators.Concrete
{
    [Serializable]
    public class LastNameValidator : IValidator
    {
        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var type = typeof(User);
            var propertyInfo = type.GetProperty("LastName");

            if (propertyInfo != null)
            {
                var attributeNotNullOrEmpty =
                    propertyInfo.GetCustomAttribute(typeof(ValidateNotNullOrEmptyOrWhiteSpaceAttribute));
                var attributeMaxLength = propertyInfo.GetCustomAttribute<ValidateMaxLengthAttribute>();
                var attributeRegex = propertyInfo.GetCustomAttribute<ValidateRegexAttribute>();

                if (attributeNotNullOrEmpty != null)
                {
                    if (string.IsNullOrWhiteSpace(user.LastName))
                    {
                        throw new LastNameNullEmptyOrWhitespace("LastName is null, empty or whitespace.");
                    }
                }

                if (user.LastName.Length > attributeMaxLength?.Length)
                {
                    throw new LastNameExceedsLengthLimitsException(nameof(user));
                }

                if (attributeRegex != null)
                {
                    var regex = new Regex(attributeRegex.Pattern);

                    var st = regex.Match(user.LastName);

                    if (!regex.IsMatch(user.LastName))
                    {
                        throw new LastNameNotMatchPatternException(nameof(user));
                    }
                }
            }
        }
    }
}
