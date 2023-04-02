using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Common.Exceptions
{
    public class PersistEntityException : ApplicationException
    {
        public PersistEntityException() : this(string.Empty, null) { }
        public PersistEntityException(string message) : this(message, null) { }
        public PersistEntityException(string message, Exception innerException) : base(message, innerException) { }
    }
}
