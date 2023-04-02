using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class LogDataViewModel
    {
        public string fechaInicial { get; set; }

        public string fechaFinal { get; set; }

        public string horaInicial { get; set; }

        public string horaFinal { get; set; }
        public IEnumerable<LogViewModel> Registros { get; set; }

        public IEnumerable<string> Encabezados { get; set; }

        public LogDataViewModel()
        {
            Encabezados = new List<string>() { "Id", "Fecha del Evento", "Id Usuario", "Aplicación", "Area", "Sección", "Acción", "Objetivo", "Entidad", "Prioridad", "Comentario" };
        }

        public void AddLog(LogViewModel Log)
        {
            LogViewModel register = new LogViewModel();

            register.Id = Log.Id;
            register.FechaEvento = Log.FechaEvento;
            register.IdUsuario = Log.IdUsuario;
            register.Aplicacion = Log.Aplicacion;
            register.Area = Log.Area;
            register.Seccion = Log.Seccion;
            register.Accion = Log.Accion;
            register.Objetivo = Log.Objetivo;
            register.Entidad = Log.Entidad;
            register.Prioridad = Log.Prioridad;
            register.Comentario = Log.Comentario;

            List<LogViewModel> Lista = new List<LogViewModel>();
            Lista.Add(register);

            Registros = Lista;
        }

    }
}