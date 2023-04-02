using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class MessageResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public object Payload { get; set; }
    }
}
