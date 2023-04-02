using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.Business.Managers;
using KAIROSV2.WebApp.Identity.Authorization;
using KAIROSV2.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KAIROSV2.WebApp.Controllers
{
    [PermissionsAuthorize(Permissions.SubModuloLog)]
    public class LogController : BaseController
    {
        private const string Vista = "Log";
        private const string TablaLog = "T_Log";

        public LogController()
        {
            Area = "Configuración";
        }

        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaLog, $"Ingreso a vista {Vista}");

            DateTime StartDate = DateTime.Now.AddDays(-7);
            DateTime FinishDate = DateTime.Now;

            return View(new LogDataViewModel
            {
                fechaInicial = StartDate.ToShortDateString(),
                horaInicial = StartDate.ToShortTimeString(),
                fechaFinal = FinishDate.ToShortDateString(),
                horaFinal = FinishDate.ToShortTimeString(),
                Registros = FindLogRegistersByDate(StartDate, FinishDate).OrderByDescending(e => e.FechaEvento ).ToList()
            });
        }
          
        public IActionResult FindRegisters(LogDataViewModel logViewModel)
        {
            DateTime HoraInicial = DateTime.Parse(logViewModel.horaInicial);
            DateTime HoraFinal = DateTime.Parse(logViewModel.horaFinal);

            DateTime StartDate = Convert.ToDateTime(logViewModel.fechaInicial).AddHours(HoraInicial.Hour).AddMinutes(HoraInicial.Minute).AddSeconds(HoraInicial.Second);
            DateTime FinishDate = Convert.ToDateTime(logViewModel.fechaFinal).AddHours(HoraFinal.Hour).AddMinutes(HoraFinal.Minute).AddSeconds(HoraFinal.Second);


            if (FinishDate > StartDate)
                return PartialView("_LogList", FindLogRegistersByDate(StartDate, FinishDate).OrderByDescending(e => e.FechaEvento).ToList());
            else
                return PartialView("_LogList", logViewModel);

        }        

        [HttpPost]
        public IActionResult BuscarRegistros([FromBody] LogDataViewModel datosbuscar)
        {
            var response = new MessageResponse();
            response.Result = false;


            DateTime HoraInicial = DateTime.Parse(datosbuscar.horaInicial);
            DateTime HoraFinal = DateTime.Parse(datosbuscar.horaFinal);

            DateTime StartDate = Convert.ToDateTime(datosbuscar.fechaInicial).AddHours(HoraInicial.Hour).AddMinutes(HoraInicial.Minute).AddSeconds(HoraInicial.Second);
            DateTime FinishDate = Convert.ToDateTime(datosbuscar.fechaFinal).AddHours(HoraFinal.Hour).AddMinutes(HoraFinal.Minute).AddSeconds(HoraFinal.Second);


            var viewModel = new LogDataViewModel()
            {
                fechaInicial = StartDate.ToShortDateString(),
                horaInicial = StartDate.ToShortTimeString(),
                fechaFinal = FinishDate.ToShortDateString(),
                horaFinal = FinishDate.ToShortTimeString(),
                Registros = new List<LogViewModel>()
            };

            if (FinishDate > StartDate)
            {
                viewModel.Registros = FindLogRegistersByDate(StartDate, FinishDate).OrderByDescending(e => e.FechaEvento).ToList();

                response.Result = true;
                response.Message = "Datos Consultados";
            }
            else
            {
                response.Message = "La fecha de inicio es mayor que la fecha final";
            }

            return PartialView("_LogList", viewModel);

        }

        private IEnumerable<LogViewModel> FindLogRegistersByDate(DateTime StartDate, DateTime FinishDate)
        {            
            var datos = Logger.ObtenerDatosPorFechas(StartDate, FinishDate);
            var Acciones = Logger.ObtenerLogAcciones();            
            var Prioridades = Logger.ObtenerLogPrioridades();            

            List<LogViewModel> LogData = new List<LogViewModel>();

            foreach (var registro in datos)
            {
                var Log = new LogViewModel()
                {
                    Id = registro.Id,
                    IdUsuario = registro.IdUsuario,
                    Aplicacion = registro.Aplicacion,
                    Seccion = registro.Seccion,
                    Area = registro.Area,
                    Entidad = registro.Entidad,
                    Comentario = registro.Comentario,
                    FechaEvento = registro.FechaEvento,
                    Objetivo = registro.Objetivo,
                    Accion = Acciones.Find(e => e.Id == registro.Accion).Accion,
                    Prioridad = Prioridades.Find(e => e.Id == registro.Prioridad).Prioridad

                };

                LogData.Add(Log);
            }

            var Datos = LogData.OrderByDescending(e => e.FechaEvento).ToList();

            return Datos;
        }

        [HttpPost]
        public async Task<IActionResult> InsertLogExampleAsync([FromBody] LogDataViewModel logViewModel)
        {

            TLog record = new TLog()
            {
                IdUsuario = "John",
                Aplicacion = "KAIROSV2",
                Area = "Configuración",
                Seccion = "Log de Eventos",
                Objetivo = "Pruebas",
                Entidad = "T_Log",
                Accion = 1,
                Comentario = "Esta es una prueba de Insercion",
                FechaEvento = DateTime.Now,
                Prioridad = 1
            };

            logViewModel.fechaInicial = DateTime.Now.AddDays(-200).ToShortDateString();
            logViewModel.horaInicial = DateTime.Now.AddDays(-200).ToShortTimeString();
            logViewModel.fechaFinal = DateTime.Now.ToShortDateString();
            logViewModel.horaFinal = DateTime.Now.ToShortTimeString();

            //List<TLog> datos = new List<TLog>();
            //datos.Add(record);
            //logViewModel.Registros = datos;


            var log = await Logger.InsertarLogAsync(record.IdUsuario, record.Aplicacion, record.Area, record.Seccion, record.Objetivo, record.Entidad, (LogAcciones)record.Accion, record.Comentario, (LogPrioridades)record.Prioridad);
            //await InsertarLogAsync(log);
            
            
            //await InsertarLogAsync(record.IdUsuario, record.Aplicacion, record.Area, record.Seccion, record.Objetivo, record.Entidad, record.Accion, record.Comentario,  record.Prioridad );

            LogDataViewModel data = new LogDataViewModel();
            data.Registros = FindLogRegistersByDate(Convert.ToDateTime(logViewModel.fechaInicial), Convert.ToDateTime(logViewModel.fechaFinal));

            //return PartialView("_LogList", data);
            return View("Index", data);

        }

        [HttpPost]
        public IActionResult InsertarLogEjemplo([FromBody] LogDataViewModel logViewModel)
        {

            TLog record = new TLog()
            {
                IdUsuario = "John",
                Aplicacion = "KAIROSV2",
                Area = "Log",
                Seccion = "Log de Eventos",
                Objetivo = "Pruebas",
                Entidad = "T_Log",
                Accion = 1,
                Comentario = "Esta es una prueba de Insercion",
                FechaEvento = DateTime.Now,
                Prioridad = 1
            };



            logViewModel.fechaInicial = DateTime.Now.AddDays(-200).ToShortDateString();
            logViewModel.horaInicial = DateTime.Now.AddDays(-200).ToShortTimeString();
            logViewModel.fechaFinal = DateTime.Now.ToShortDateString();
            logViewModel.horaFinal = DateTime.Now.ToShortTimeString();

            List<TLog> datos = new List<TLog>();
            datos.Add(record);

            //logViewModel.Registros = datos;

            InsertarLog(record);
            InsertarLog(record.IdUsuario, record.Aplicacion, record.Area, record.Seccion, record.Objetivo, record.Entidad, record.Accion, record.Comentario, record.Prioridad);

            LogDataViewModel data = new LogDataViewModel();
            data.Registros = FindLogRegistersByDate(Convert.ToDateTime(logViewModel.fechaInicial), Convert.ToDateTime(logViewModel.fechaFinal));

            //return PartialView("_LogList", data);
            return View("Index", data);

        }

        public Task<bool> InsertarLogAsync(string Usuario, string Aplicacion, string Area , string Seccion , string Objetivo , string Entidad , int Accion, string Comentario , int Prioridad)
        {
            TLog record = new TLog()
            {
                IdUsuario = Usuario,
                Aplicacion = Aplicacion,
                Area = Area,
                Seccion = Seccion,
                Objetivo = Objetivo,
                Entidad = Entidad,
                Accion = Accion,
                Comentario = Comentario,
                FechaEvento = DateTime.Now,
                Prioridad = Prioridad
            };

            return Logger.AddLogAsync(record);
            
        }

        public Task<bool> InsertarLogAsync(TLog record)
        {
            return Logger.AddLogAsync(record);
        }

        public bool InsertarLog(string Usuario, string Aplicacion, string Area, string Seccion, string Objetivo, string Entidad, int Accion, string Comentario, int Prioridad)
        {
            TLog record = new TLog()
            {
                IdUsuario = Usuario,
                Aplicacion = Aplicacion,
                Area = Area,
                Seccion = Seccion,
                Objetivo = Objetivo,
                Entidad = Entidad,
                Accion = Accion,
                Comentario = Comentario,
                FechaEvento = DateTime.Now,
                Prioridad = Prioridad
            };

            return Logger.AddLog(record);

        }

        public bool InsertarLog(TLog record)
        {
            return Logger.AddLog(record);
        }

    }
}
