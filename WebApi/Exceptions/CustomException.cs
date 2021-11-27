using System;
using System.Runtime.Serialization;

namespace WebApi.Exceptions
{
    public class CustomException : Exception
    {
        private string _codeError;

        public string CodeError
        {
            get
            {
                return _codeError;
            }
        }

        public CustomException()
        {
        }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CustomException(string message, string codeError) : base(message)
        {
            _codeError = codeError;
        }
    }
}
