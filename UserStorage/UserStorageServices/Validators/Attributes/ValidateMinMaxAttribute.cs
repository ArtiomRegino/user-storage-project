using System;

namespace UserStorageServices.Validators.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateMinMaxAttribute : Attribute
    {
        public int Min { get; }

        public int Max { get; }

        public ValidateMinMaxAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}
