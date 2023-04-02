using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class TerminalesCompañiasDisponiblesViewModel
    {
        public string IdCompañia { get; set; }
        public string Nombre { get; set; }
        public string SalesOrganization { get; set; }
        public string DistributionChannel { get; set; }
        public string Division { get; set; }
        public string SupplierType { get; set; }
        public string CodigoSicom { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }
        public bool Seleccionado { get; set; }

    }
}

