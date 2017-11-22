using System;

namespace UserStorageServices.Validators.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMaxLengthAttribute : Attribute
    {
        public int Length { get; }

        public ValidateMaxLengthAttribute(int length)
        {
            Length = length;
        }
    }
}
