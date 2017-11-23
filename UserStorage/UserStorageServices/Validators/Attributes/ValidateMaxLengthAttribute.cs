using System;

namespace UserStorageServices.Validators.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMaxLengthAttribute : Attribute
    {
        public ValidateMaxLengthAttribute(int length)
        {
            Length = length;
        }

        public int Length { get; }
    }
}
