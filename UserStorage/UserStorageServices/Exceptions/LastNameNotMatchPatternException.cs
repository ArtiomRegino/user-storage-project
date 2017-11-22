using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Exceptions
{
    [Serializable]
    public class LastNameNotMatchPatternException : Exception
    {
        public LastNameNotMatchPatternException()
        {
        }

        public LastNameNotMatchPatternException(string message) : base(message)
        {
        }

        public LastNameNotMatchPatternException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LastNameNotMatchPatternException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
