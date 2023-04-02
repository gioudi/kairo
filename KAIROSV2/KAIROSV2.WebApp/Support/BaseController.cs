using AutoMapper;
using KAIROSV2.Business.Contracts.Managers;
using Microsoft.Extensions.DependencyInjection;
using KAIROSV2.WebApp.Identity;
using Microsoft.AspNetCore.Mvc;
using KAIROSV2.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Identity;
using KAIROSV2.Business.Entities;

namespace KAIROSV2.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        private ILogManager _logManager;
        private IMapper _mapper;
        private UserManager<TUUsuario> _userManager;

        protected ILogManager Logger => _logManager ??= HttpContext?.RequestServices.GetRequiredService<ILogManager>();
        protected IMapper Mapper => _mapper ??= HttpContext?.RequestServices.GetRequiredService<IMapper>();
        protected UserManager<TUUsuario> UserManager => _userManager ??= HttpContext?.RequestServices.GetRequiredService<UserManager<TUUsuario>>();
        protected string Area { get; set; }

        #region Loggin options
        public virtual void LogInformacion(LogAcciones accion, string vista, string tabla, string comentario) 
        {
            Logger.LogInformacion(accion, UserManager.GetUserId(User), "Kairos2", Area, ControllerContext.ActionDescriptor.ControllerName, vista, tabla, comentario);
        }
        public virtual void LogInformacionActualizar(string vista, string tabla, string comentario, object anterior, object nuevo)
        {
            Logger.LogInformacionActualizar(UserManager.GetUserId(User), "Kairos2", Area, ControllerContext.ActionDescriptor.ControllerName, vista, tabla, comentario, anterior, nuevo);
        }
        public virtual void LogAdvertencia(LogAcciones accion, string vista, string tabla, string comentario) 
        {
            Logger.LogAdvertencia(accion, UserManager.GetUserId(User), "Kairos2", Area, ControllerContext.ActionDescriptor.ControllerName, vista, tabla, comentario);
        }
        public virtual void LogError(LogAcciones accion, string vista, string tabla, string comentario, Exception error)
        {
            Logger.LogError(accion, UserManager.GetUserId(User), "Kairos2", Area, ControllerContext.ActionDescriptor.ControllerName, vista, tabla, comentario, error);
        }
        #endregion
    }
}
