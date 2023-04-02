using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System.Linq;
using KAIROSV2.Business.Contracts.Engines;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.Business.Managers
{
    /// <summary>
    /// Maneja la logica de negocio para los Compañias
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para la entidad Compañia, las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir Compañias.
    /// </remarks>
    public class CompañiasManager : ManagerBase, ICompañiasManager
    {
        private readonly ICompañiasRepository _CompañiasRepository;

        public CompañiasManager(ICompañiasRepository CompañiasRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _CompañiasRepository = CompañiasRepository;
        }

        /// <summary>
        /// Obtiene el Compañia incluyendo su imagen
        /// </summary>
        /// <param name="idCompañia">Id del Compañia</param>
        /// <returns>Compañia</returns>
        public TCompañia ObtenerCompañia(string idCompañia)
        {
            return _CompañiasRepository.Obtener(idCompañia);
        }

        public Task<TCompañia> ObtenerCompañiaAsync(string idCompañia)
        {
            return _CompañiasRepository.ObtenerAsync(idCompañia);
        }

        /// <summary>
        /// Obtiene todos los Compañias del sistema incluyendo la imagen de cada uno
        /// </summary>
        /// <returns>Compañias del sistema</returns>
        public Task<IEnumerable<TCompañia>> ObtenerCompañiasAsync()
        {
            return _CompañiasRepository.ObtenerTodasAsync();
        }

        public IEnumerable<TCompañia> ObtenerCompañias()
        {
            return _CompañiasRepository.ObtenerTodas();
        }


        /// <summary>
        /// Crean el Compañia en el sistema
        /// </summary>
        /// <param name="Compañia">Entidad Compañia para crear</param>
        /// <returns>True si creo el Compañia, Flase si el Compañia ya existe</returns>
        public bool CrearCompañia(TCompañia Compañia)
        {
            if (_CompañiasRepository.Existe(Compañia.IdCompañia))
                return false;
            else
            {
                _CompañiasRepository.Add(Compañia);
                LogInformacion(LogAcciones.Insertar, "Configuración", "Compañías", "Compañías", "T_Compañias", "Compañía " + Compañia?.IdCompañia + " creada");
            }

            return true;
        }

        /// <summary>
        /// Actualiza los datos del Compañia
        /// </summary>
        /// <param name="Compañia">Entidad Compañia para actualizar</param>
        /// <returns>True si se actualizo el Compañia, False si no existe el Compañia</returns>
        public bool ActualizarCompañia(TCompañia Compañia)
        {
            if (!_CompañiasRepository.Existe(Compañia.IdCompañia))
                return false;
            else
            {
                var compUpdate = _CompañiasRepository.Obtener(Compañia.IdCompañia);
                Compañia.FilaId = compUpdate.FilaId;   
                _CompañiasRepository.Update(Compañia);
                LogInformacion(LogAcciones.Actualizar, "Configuración", "Compañías", "Compañías", "T_Compañias", "Compañía " + Compañia?.IdCompañia + " actualizada");
            }

            return true;
        }

        public bool BorrarCompañia(string idCompañia)
        {
            var result = false;

            if (_CompañiasRepository.Existe(idCompañia))
            {
                _CompañiasRepository.Remove(idCompañia);
                result = true;
                LogInformacion(LogAcciones.Eliminar, "Configuración", "Compañías", "Compañías", "T_Compañias", "Compañía " + idCompañia + " eliminada");
            }

            return result;
        }

        public async Task<bool> BorrarCompañiaAsync(string idCompañia)
        {
            var result = false;

            if ( _CompañiasRepository.Existe(idCompañia))
            {
                _CompañiasRepository.Remove(idCompañia);
                result = true;
                LogInformacion(LogAcciones.Eliminar, "Configuración", "Compañías", "Compañías", "T_Compañias", "Compañía " + idCompañia + " eliminada");
            }

            return result;
        }
    }

}
