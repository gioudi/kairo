using KAIROSV2.Business.Entities;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.Support.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionProcesamientoArchivosViewModel
    {
        public string Titulo { get; set; }
        public string Accion { get; set; }
        public bool Paso1 { get; set; }
        public bool Paso2 { get; set; }
        public bool Paso3 { get; set; }
        public MapeoArchivoDatosIniciales MapeoArchivoPaso1 { get; set; }
        public MapeoArchivoPrevisualizacion MapeoArchivoPaso2 { get; set; }
        public MapeoArchivoTablas MapeoArchivoPaso3 { get; set; }

    }
}
