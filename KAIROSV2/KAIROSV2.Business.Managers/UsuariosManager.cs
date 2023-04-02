using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System.Linq;
using KAIROSV2.Business.Contracts.Engines;
using Microsoft.AspNetCore.Http;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.Business.Managers
{
    /// <summary>
    /// Maneja la logica de negocio para los usuarios
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para la entidad usuario, las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir usuarios.
    /// </remarks>
    public class UsuariosManager : ManagerBase, IUsuariosManager
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosManager(IUsuariosRepository usuariosRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _usuariosRepository = usuariosRepository;
        }

        /// <summary>
        /// Obtiene el usuario incluyendo su imagen
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns>Usuario</returns>
        public TUUsuario ObtenerUsuario(string idUsuario)
        {
            return _usuariosRepository.Get(idUsuario, "TUUsuarioImagen");
        }

        /// <summary>
        /// Obtiene todos los usuarios del sistema incluyendo la imagen de cada uno
        /// </summary>
        /// <returns>Usuarios del sistema</returns>
        public IEnumerable<TUUsuario> ObtenerUsuarios()
        {
            return _usuariosRepository.GetAll("TUUsuarioImagen");
        }

        /// <summary>
        /// Crean el usuario en el sistema
        /// </summary>
        /// <param name="usuario">Entidad usuario para crear</param>
        /// <returns>True si creo el usuario, Flase si el usuario ya existe</returns>
        public bool CrearUsuario(TUUsuario usuario)
        {
            if (_usuariosRepository.Exists(usuario.IdUsuario))
                return false;
            else
            {
                _usuariosRepository.Add(usuario);
                LogInformacion(LogAcciones.Insertar, "Administración", "Usuarios", "Usuarios", "T_U_Usuarios", $"Usuario {usuario?.IdUsuario} creado.");
            }

            return true;
        }

        /// <summary>
        /// Actualiza los datos del usuario
        /// </summary>
        /// <param name="usuario">Entidad usuario para actualizar</param>
        /// <returns>True si se actualizo el usuario, False si no existe el usuario</returns>
        public bool ActualizarUsuario(TUUsuario usuario)
        {
            if (!_usuariosRepository.Exists(usuario.IdUsuario))
                return false;
            else
            {
                _usuariosRepository.Update(usuario);
                LogInformacion(LogAcciones.Insertar, "Administración", "Usuarios", "Usuarios", "T_U_Usuarios", $"Usuario {usuario?.IdUsuario} actualizado.");
            }

            return true;
        }
    }

}
