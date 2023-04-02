using AutoMapper;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Managers
{
    public abstract class ManagerBase
    {
        private string _userId;

        protected ILogManager Logger;
        protected IMapper Mapper;

        public ManagerBase(IHttpContextAccessor httpContextAccessor)
        {
            Logger = httpContextAccessor?.HttpContext.RequestServices.GetRequiredService<ILogManager>();
            Mapper = httpContextAccessor?.HttpContext.RequestServices.GetRequiredService<IMapper>();
            _userId = httpContextAccessor?.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        #region Loggin options
        public virtual void LogInformacion(LogAcciones accion, string area, string seccion, string vista, string tabla, string comentario)
        {
            Logger.LogInformacion(accion, _userId, "Kairos2", area, seccion, vista, tabla, comentario);
        }
        public virtual void LogInformacionActualizar(string vista, string area, string seccion, string tabla, string comentario, object anterior, object nuevo)
        {
            Logger.LogInformacionActualizar(_userId, "Kairos2", area, seccion, vista, tabla, comentario, anterior, nuevo);
        }
        public virtual void LogAdvertencia(LogAcciones accion, string area, string seccion, string vista, string tabla, string comentario)
        {
            Logger.LogAdvertencia(accion, _userId, "Kairos2", area, seccion, vista, tabla, comentario);
        }
        public virtual void LogError(LogAcciones accion, string area, string seccion, string vista, string tabla, string comentario, Exception error)
        {
            Logger.LogError(accion, _userId, "Kairos2", area, seccion, vista, tabla, comentario, error);
        }
        #endregion
    }
}
