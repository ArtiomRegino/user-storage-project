using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Exceptions
{
    [Serializable]
    public class AgeExceedsLimitsException : Exception
    {
        public AgeExceedsLimitsException()
        {
        }

        public AgeExceedsLimitsException(string message) : base(message)
        {
        }

        public AgeExceedsLimitsException(string message, Exception inner) : base(message, inner)
        {
        }

        protected AgeExceedsLimitsException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
