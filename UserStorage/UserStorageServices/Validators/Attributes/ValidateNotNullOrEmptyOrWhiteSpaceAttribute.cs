using System;

namespace UserStorageServices.Validators.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateNotNullOrEmptyOrWhiteSpaceAttribute : Attribute
    {
    }
}
