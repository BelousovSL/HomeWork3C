using System;
using System.Runtime.Serialization;

namespace WebApi.Exceptions
{
    [Serializable]
    internal class NotFoundException : CustomException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException(string message, string additionalCodeError) : base(message, additionalCodeError)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
