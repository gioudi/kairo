using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class ManagerResult<T> where T : class
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public List<string> Failures { get; set; }
        public T EntityPayload { get; set; }

        public string GetFailuresToString()
        {
            return string.Join(",", Failures);
        }
    }

    public class ManagerResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public List<string> Failures { get; set; }
        public string GetFailuresToString()
        {
            return string.Join(",", Failures);
        }
    }
}
