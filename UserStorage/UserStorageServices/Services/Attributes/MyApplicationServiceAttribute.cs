using System;

namespace UserStorageServices.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyApplicationServiceAttribute : Attribute
    {
        public MyApplicationServiceAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
