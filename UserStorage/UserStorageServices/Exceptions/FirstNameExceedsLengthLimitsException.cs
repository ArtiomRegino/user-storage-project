using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Exceptions
{
    [Serializable]
    public class FirstNameExceedsLengthLimitsException : Exception
    {
        public FirstNameExceedsLengthLimitsException()
        {
        }

        public FirstNameExceedsLengthLimitsException(string message) : base(message)
        {
        }

        public FirstNameExceedsLengthLimitsException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FirstNameExceedsLengthLimitsException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
