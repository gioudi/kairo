using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface ILogManager
    {
        IEnumerable<TLog> ObtenerDatosPorFechas(DateTime startdate, DateTime finishdate);

        Task<bool> AddLogAsync(TLog user, CancellationToken cancellationToken = default);

        public bool AddLog(TLog register);

        List<TLogAcciones> ObtenerLogAcciones();

        List<TLogPrioridades> ObtenerLogPrioridades();

        Task<bool> InsertarLogAsync(string Usuario, string Aplicacion, string Area, string Seccion, string Objetivo, string Entidad, LogAcciones Accion, string Comentario, LogPrioridades Prioridad);

        //Task<TLog> InsertarLogAsync(string Usuario, string Aplicacion, string Area, string Seccion, string Objetivo, string Entidad, int Accion, string Comentario, int Prioridad);

        Task<bool> InsertarLogAsync(TLog record);

        bool InsertarLog(string Usuario, string Aplicacion, string Area, string Seccion, string Objetivo, string Entidad, LogAcciones Accion, string Comentario, LogPrioridades Prioridad);

        bool InsertarLog(TLog record);

        string ManejoErrores(Exception e);      

        string SerializarEntidad(object Entidad);

        bool LogAdvertencia(LogAcciones accion, string usuario, string aplicacion, string area, string seccion, string objetivo, string entidad, string comentario);
        bool LogError(LogAcciones accion, string usuario, string aplicacion, string area, string seccion, string objetivo, string entidad, string comentario, Exception error);
        bool LogInformacion(LogAcciones accion, string usuario, string aplicacion, string area, string seccion, string objetivo, string entidad, string comentario);
        bool LogInformacionActualizar(string usuario, string aplicacion, string area, string seccion, string objetivo, string entidad, string comentario, object anterior, object nuevo);

    }
}
