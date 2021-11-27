using System;
using System.Runtime.Serialization;

namespace WebApi.Exceptions
{

    [Serializable]
    internal class ConflictException : CustomException
    {
        public ConflictException()
        {
        }

        public ConflictException(string message) : base(message)
        {
        }

        public ConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ConflictException(string message, string additionalCodeError) : base(message, additionalCodeError)
        {
        }

        protected ConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
