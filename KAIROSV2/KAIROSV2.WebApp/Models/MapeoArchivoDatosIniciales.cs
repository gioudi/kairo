using KAIROSV2.Business.Entities;
using KAIROSV2.WebApp.Models.Enums;
using KAIROSV2.WebApp.Support.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class MapeoArchivoDatosIniciales
    {
        [Required(ErrorMessage = "Id Mapeo es obligatorio")]
        [MinLength(5, ErrorMessage = "La longitud mínima son 5 caracteres")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string IdMapeo { get; set; }

        [Required(ErrorMessage = "Descripción es obligatorio")]
        [MinLength(5, ErrorMessage = "La longitud mínima son 5 caracteres")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        public string Descripcion { get; set; }

        public string RutaArchivo { get; set; }

        public bool Nuevo { get; set; }

        [Required(ErrorMessage = "Separador es obligatorio")]
        [EnumDataType(typeof(SeparadorArchivoEnum))]
        public SeparadorArchivoEnum Separador { get; set; }

        [RequiredIf("Separador", (int)SeparadorArchivoEnum.Otro, ErrorMessage = "Otro carácter de separación es obligatorio")]
        public string OtroCaracter { get; set; }
        public bool Lectura { get; set; }

        [Required(ErrorMessage = "Es necesario cargar un archivo")]
        [DataType(DataType.Upload)]
        [MaxFileSize(800)]
        [AllowedExtensions(new string[] { ".txt", ".csv" })]
        public IFormFile Archivo { get; set; }

        public TProcesamientoArchivosMst ExtraerMapeo()
        {
            var _mapeo = new TProcesamientoArchivosMst()
            {
                IdMapeo = IdMapeo,
                RutaArchivo = RutaArchivo,
                Separador = (Char)Separador,
                Descripcion = Descripcion,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now
            };
            return _mapeo;
        }

        public void AsignarMapeo(TProcesamientoArchivosMst _mapeoEncabezado)
        {
            IdMapeo = _mapeoEncabezado?.IdMapeo;

            RutaArchivo = _mapeoEncabezado?.RutaArchivo;
            Separador = (SeparadorArchivoEnum)_mapeoEncabezado.Separador;
        }
    }
}
