using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Exceptions
{
    [Serializable]
    public class LastNameNullEmptyOrWhitespace : Exception
    {
        public LastNameNullEmptyOrWhitespace()
        {
        }

        public LastNameNullEmptyOrWhitespace(string message) : base(message)
        {
        }

        public LastNameNullEmptyOrWhitespace(string message, Exception inner) : base(message, inner)
        {
        }

        protected LastNameNullEmptyOrWhitespace(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
