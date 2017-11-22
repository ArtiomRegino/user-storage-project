using System;

namespace UserStorageServices.Validators.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateRegexAttribute : Attribute
    {
        public string Pattern { get; }

        public ValidateRegexAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}
