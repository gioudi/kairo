using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System.Linq;
using KAIROSV2.Business.Contracts.Engines;
using System.Threading.Tasks;
using KAIROSV2.Business.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace KAIROSV2.Business.Managers
{
    /// <summary>
    /// Maneja la logica de negocio para los areas
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para la entidad area, las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir areas.
    /// </remarks>
    public class AreasManager : ManagerBase, IAreasManager
    {
        private readonly IAreasRepository _areasRepository;

        public AreasManager(IAreasRepository areasRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _areasRepository = areasRepository;
        }

        /// <summary>
        /// Obtiene el area incluyendo su imagen
        /// </summary>
        /// <param name="idarea">Id del area</param>
        /// <returns>area</returns>
        public TArea ObtenerArea(string idarea)
        {
            return _areasRepository.Obtener(idarea);
        }

        public Task<TArea> ObtenerAreaAsync(string idarea)
        {
            return _areasRepository.ObtenerAsync(idarea);
        }

        /// <summary>
        /// Obtiene todos los areas del sistema incluyendo la imagen de cada uno
        /// </summary>
        /// <returns>areas del sistema</returns>
        public IEnumerable<TArea> ObtenerAreas()
        {
            return _areasRepository.ObtenerTodas();
        }

        /// <summary>
        /// Crean el area en el sistema
        /// </summary>
        /// <param name="area">Entidad area para crear</param>
        /// <returns>True si creo el area, Flase si el area ya existe</returns>
        public bool CrearArea(TArea area)
        {
            if (_areasRepository.Existe(area.IdArea))
                return false;
            else
            {
                _areasRepository.Add(area);
                LogInformacion(LogAcciones.Insertar, "Configuración", "Áreas", "Áreas", "T_Areas", $"Área " + area?.IdArea + " creada");
            }

            return true;
        }

        /// <summary>
        /// Actualiza los datos del área
        /// </summary>
        /// <param name="area">Entidad área para actualizar</param>
        /// <returns>True si se actualizo el área, False si no existe el área</returns>
        public bool ActualizarArea(TArea area)
        {
            try
            {
                if (!_areasRepository.Existe(area.IdArea))
                    return false;
                else
                {
                    _areasRepository.Update(area);
                    LogInformacion(LogAcciones.Actualizar, "Configuración", "Áreas", "Áreas", "T_Areas", "Área " + area?.IdArea + " actualizada");
                }

            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Configuración", "Áreas", "Áreas", "T_Areas", "Área " + area?.IdArea + " no actualizada.", e);
                return false;
            }

            return true;
        }

        public bool BorrarArea(string idArea)
        {
            var result = false;
            try
            {
                if (_areasRepository.Existe(idArea))
                {
                    _areasRepository.Remove(idArea);
                    LogInformacion(LogAcciones.Eliminar, "Configuración", "Áreas", "Áreas", "T_Areas", $"Área {idArea} eliminada.");
                    result = true;
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Eliminar, "Configuración", "Áreas", "Áreas", "T_Areas", $"Área {idArea} no eliminada.", e);
                result = false;
            }

            return result;
        }

        public async Task<bool> BorrarAreaAsync(string idArea)
        {
            var result = false;

            try
            {
                if (_areasRepository.Existe(idArea))
                {
                    _areasRepository.Remove(idArea);
                    LogInformacion(LogAcciones.Eliminar, "Configuración", "Áreas", "Áreas", "T_Areas", $"Área {idArea} eliminada");
                    result = true;
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Eliminar, "Configuración", "Áreas", "Áreas", "T_Areas", $"Área {idArea} eliminada", e);
                result = false;
            }

            return result;
        }
    }

}
