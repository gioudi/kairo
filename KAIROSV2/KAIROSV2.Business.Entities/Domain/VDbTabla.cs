using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class VDbTabla
    {
        public string Name { get; set; }
        public int ObjectId { get; set; }
        public int? PrincipalId { get; set; }
        public int SchemaId { get; set; }
    }
}
