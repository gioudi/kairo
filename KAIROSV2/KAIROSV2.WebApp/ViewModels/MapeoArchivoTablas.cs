using KAIROSV2.Business.Entities;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.Models.Enums;
using KAIROSV2.WebApp.Support.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class MapeoArchivoTablas
    {
        #region Propiedades
        [RequiredIf("Nuevo", false, ErrorMessage = "Id Mapeo es obligatorio")]
        [MinLength(5, ErrorMessage = "La longitud mínima son 5 caracteres")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string IdMapeo { get; set; }

        [RequiredIf("Nuevo", false, ErrorMessage = "Descripción es obligatorio")]
        [MinLength(5, ErrorMessage = "La longitud mínima son 5 caracteres")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Descripcion { get; set; }

        [EnumDataType(typeof(SeparadorArchivoEnum))]
        public SeparadorArchivoEnum Separador { get; set; }
        public string OtroCaracter { get; set; }
        public bool Nuevo { get; set; }
        public bool Lectura { get; set; }
        public List<string> ColumnasArchivo { get; set; }

        [Required(ErrorMessage = "Es necesario escoger alguna tabla")]
        public List<TablasSistema> TablasSistemas { get; set; }

        public List<TablaSistemaColumna> CurrColumnas;

        public List<string> TablasSistemaListado;

        public string CurrTabla;

        #endregion

        public TProcesamientoArchivosMst ExtraerMapeo(string usuario)
        {
            var _mapeo = new TProcesamientoArchivosMst()
            {
                IdMapeo = IdMapeo,
                Separador = (Char)Separador,
                Descripcion = Descripcion,
                EditadoPor = usuario,
                UltimaEdicion = DateTime.Now,
                NombreArchivo = ""

            };
            return _mapeo;
        }
    }


    public class TablasSistema
    {
        [Required(ErrorMessage = "Debes seleccionar una tabla")]
        public string NombreTabla { get; set; }
        [Required(ErrorMessage = "Prioridad es obligatorio")]
        [Range(1, 100, ErrorMessage = "debe ser menor a 100")]
        public int Prioridad { get; set; }

        [Required(ErrorMessage = "Es necesario escoger alguna columna")]
        public List<TablaSistemaColumna> Columnas { get; set; }
        public List<string> NombresColumnas { get; set; }
    }

    public class TablaSistemaColumna
    {
        [Required(ErrorMessage = "Nombre tabla es obligatorio")]
        public string Nombre { get; set; }
        public bool Llave { get; set; }
        public bool IsNull { get; set; }
        public string Tipo { get; set; }
        public int Longitud { get; set; }
        public int IndexColumnaArchivo { get; set; }
        [Required(ErrorMessage = "Columna archivo es obligatorio")]
        public string ColumnaArchivo { get; set; }
    }

}
