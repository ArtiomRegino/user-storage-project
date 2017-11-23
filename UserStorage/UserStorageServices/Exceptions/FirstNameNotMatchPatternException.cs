using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Exceptions
{
    [Serializable]
    public class FirstNameNotMatchPatternException : Exception
    {
        public FirstNameNotMatchPatternException()
        {
        }

        public FirstNameNotMatchPatternException(string message) : base(message)
        {
        }

        public FirstNameNotMatchPatternException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FirstNameNotMatchPatternException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
