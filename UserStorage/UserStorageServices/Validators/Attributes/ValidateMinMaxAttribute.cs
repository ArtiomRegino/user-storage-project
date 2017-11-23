using System;

namespace UserStorageServices.Validators.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMinMaxAttribute : Attribute
    {
        public ValidateMinMaxAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; }

        public int Max { get; }
    }
}
