using System;
using System.Runtime.Serialization;

namespace Template.Domain.Base
{
    /// <summary>
    /// Исключение бизнес логик
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    }
}