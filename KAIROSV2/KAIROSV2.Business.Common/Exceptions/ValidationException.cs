using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Common.Exceptions
{
    public class ValidationException: ApplicationException
    {
        public ValidationException() : this(string.Empty, null) { }
        public ValidationException(string message) : this(message, null) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
