using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Common.Exceptions
{
    public class DeleteCascadeException : ApplicationException
    {
        public DeleteCascadeException() : this(string.Empty, null) { }
        public DeleteCascadeException(string message) : this(message, null) { }
        public DeleteCascadeException(string message, Exception innerException) : base(message, innerException) { }
    }
}
