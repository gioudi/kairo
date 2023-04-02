using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class VDbColumna
    {
        public int ObjectId { get; set; }
        public string Name { get; set; }
        public int ColumnId { get; set; }
        public string Tipo { get; set; }
        public int UserTypeId { get; set; }
        public short MaxLength { get; set; }
        public byte Precision { get; set; }
        public bool IsIdentity { get; set; }
        public bool? IsNullable { get; set; }
    }
}
