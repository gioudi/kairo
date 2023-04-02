using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class ProcesamientoArchivosViewModel
    {
        #region Propiedades
        public IEnumerable<string> Encabezados { get; set; }
        public string IdMapeo { get; set; }
        public string Descripcion { get; set; }
        public int NumeroTablas { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        #endregion

    }
}