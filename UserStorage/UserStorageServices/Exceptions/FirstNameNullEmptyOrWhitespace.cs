using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Exceptions
{
    [Serializable]
    public class FirstNameNullEmptyOrWhitespace : Exception
    {
        public FirstNameNullEmptyOrWhitespace()
        {
        }

        public FirstNameNullEmptyOrWhitespace(string message) : base(message)
        {
        }

        public FirstNameNullEmptyOrWhitespace(string message, Exception inner) : base(message, inner)
        {
        }

        protected FirstNameNullEmptyOrWhitespace(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
