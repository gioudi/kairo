using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System.Linq;
using KAIROSV2.Business.Contracts.Engines;
using KAIROSV2.Data;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.Business.Managers
{
    public class LogManager : ILogManager
    {
        private readonly ILogRepository _logRepository;
        private bool disposedValue;

        public LogManager(ILogRepository logsRepository)
        {
            _logRepository = logsRepository;
        }

        protected void ThrowIfDisposed()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public IEnumerable<TLog> ObtenerDatosPorFechas(DateTime startdate, DateTime finishdate)
        {
            var datos = new List<TLog>();
            try
            {
                return _logRepository.GetLogsByDates(startdate, finishdate);
            }
            catch
            {
                return datos;
            }
        }

        public async Task<bool> AddLogAsync(TLog register, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (register == null)
            {
                throw new ArgumentNullException(nameof(register));
            }
            try
            {
                await _logRepository.AddAsync(register, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<TLogAcciones> ObtenerLogAcciones()
        {
            var datos = new List<TLogAcciones>();
            try
            {
                return _logRepository.ObtenerLogAcciones().ToList();
            }
            catch 
            {
                return datos;
            }
        }

        public List<TLogPrioridades> ObtenerLogPrioridades()
        {
            var datos = new List<TLogPrioridades>();
            try
            {
                return _logRepository.ObtenerLogPrioridades().ToList();
            }
            catch
            {
                return datos;
            }
        }

        public Task<bool> InsertarLogAsync(string Usuario, string Aplicacion, string Area, string Seccion, string Objetivo, string Entidad, LogAcciones Accion, string Comentario, LogPrioridades Prioridad)
        {
            TLog record = new TLog()
            {
                IdUsuario = Usuario,
                Aplicacion = Aplicacion,
                Area = Area,
                Seccion = Seccion,
                Objetivo = Objetivo,
                Entidad = Entidad,
                Accion = (int)Accion,
                Comentario = Comentario,
                FechaEvento = DateTime.Now,
                Prioridad = (int)Prioridad
            };

            return AddLogAsync(record);

        }

        public Task<bool> InsertarLogAsync(TLog record)
        {
            return AddLogAsync(record);
        }

        public bool InsertarLog(string Usuario, string Aplicacion, string Area, string Seccion, string Objetivo, string Entidad, LogAcciones Accion, string Comentario, LogPrioridades Prioridad)
        {
            TLog record = new TLog()
            {
                IdUsuario = Usuario,
                Aplicacion = Aplicacion,
                Area = Area,
                Seccion = Seccion,
                Objetivo = Objetivo,
                Entidad = Entidad,
                Accion = (int)Accion,
                Comentario = Comentario,
                FechaEvento = DateTime.Now,
                Prioridad = (int)Prioridad
            };

            return AddLog(record);

        }

        public string ManejoErrores (Exception e)
        {
            string mensaje = "";
            try
            {
                if (e.Message != null) mensaje = e.Message;
                if (e.InnerException != null) mensaje = mensaje + e.InnerException.Message;
                
            }
            catch
            {
                mensaje = "Error no definido";
            }

            return mensaje;
        }
        public bool InsertarLog(TLog record)
        {
            return AddLog(record);
        }

        public bool LogAdvertencia(LogAcciones accion, string usuario, string aplicacion, string area, string seccion, string objetivo, string entidad, string comentario)
        {
            return AddLog(new TLog()
            {
                FechaEvento = DateTime.Now,
                IdUsuario = usuario,
                Aplicacion = aplicacion,
                Area = area,
                Seccion = seccion,
                Accion = (int)accion,
                Objetivo = objetivo,
                Entidad = entidad,
                Prioridad = (int)LogPrioridades.Informacion,
                Comentario = comentario
            });
        }
        public bool LogError(LogAcciones accion, string usuario, string aplicacion, string area, string seccion, string objetivo, string entidad, string comentario, Exception error)
        {
            comentario = $"{comentario}. Error: {ManejoErrores(error)}";

            return AddLog(new TLog()
            {
                FechaEvento = DateTime.Now,
                IdUsuario = usuario,
                Aplicacion = aplicacion,
                Area = area,
                Seccion = seccion,
                Accion = (int)accion,
                Objetivo = objetivo,
                Entidad = entidad,
                Prioridad = (int)LogPrioridades.Error,
                Comentario = comentario
            });
        }
        public bool LogInformacion(LogAcciones accion, string usuario, string aplicacion, string area, string seccion, string objetivo, string entidad, string comentario)
        {
            return AddLog(new TLog()
            {
                FechaEvento = DateTime.Now,
                IdUsuario = usuario,
                Aplicacion = aplicacion,
                Area = area,
                Seccion = seccion,
                Accion = (int)accion,
                Objetivo = objetivo,
                Entidad = entidad,
                Prioridad = (int)LogPrioridades.Informacion,
                Comentario = comentario
            });
        }
        public bool LogInformacionActualizar(string usuario, string aplicacion, string area, string seccion, string objetivo, string entidad, string comentario, object anterior, object nuevo)
        {
            comentario = $"{comentario}. Datos antiguos {SerializarEntidad(anterior)} Datos nuevos {SerializarEntidad(nuevo)}";

            return AddLog(new TLog()
            {
                FechaEvento = DateTime.Now,
                IdUsuario = usuario,
                Aplicacion = aplicacion,
                Area = area,
                Seccion = seccion,
                Accion = (int)LogAcciones.Actualizar,
                Objetivo = objetivo,
                Entidad = entidad,
                Prioridad = (int)LogPrioridades.Informacion,
                Comentario = comentario
            });
        }
        public bool AddLog(TLog register)
        {
            var result = false;

            if (register != null)
            {
                try
                {
                    _logRepository.Add(register);
                    result = true;
                }
                catch (Exception ex) { }
            }

            return result;
        }
        public string SerializarEntidad(object Entidad)
        {
            return JsonConvert.SerializeObject(Entidad, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }
}
