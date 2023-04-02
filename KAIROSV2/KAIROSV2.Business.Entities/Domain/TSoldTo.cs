using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TSoldTo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sold_to { get; set; }        
        public string Nombre_1 { get; set; }        
        public string Nombre_2 { get; set; }        
        public string Nombre_3 { get; set; }        
        public string Nombre_4 { get; set; }        
        public string Ciudad { get; set; }        
        public string Distrito { get; set; }        
        public string Codigo_Postal { get; set; }        
        public string Region { get; set; }
        public string Telefono_1 { get; set; }        
        public string Telefono_2 { get; set; }

    }
}
