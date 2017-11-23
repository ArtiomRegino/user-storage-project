using System;

namespace UserStorageServices.Validators.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateRegexAttribute : Attribute
    {
        public ValidateRegexAttribute(string pattern)
        {
            Pattern = pattern;
        }

        public string Pattern { get; }
    }
}
