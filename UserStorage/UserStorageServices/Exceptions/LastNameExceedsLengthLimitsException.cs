using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Exceptions
{
    [Serializable]
    public class LastNameExceedsLengthLimitsException : Exception
    {
        public LastNameExceedsLengthLimitsException()
        {
        }

        public LastNameExceedsLengthLimitsException(string message) : base(message)
        {
        }

        public LastNameExceedsLengthLimitsException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LastNameExceedsLengthLimitsException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
