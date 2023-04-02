using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System.Linq;
using KAIROSV2.Business.Contracts.Engines;
using System.Threading.Tasks;
using System.Threading;
using KAIROSV2.Business.Entities.DTOs;
using System.Text.Json;
using Newtonsoft.Json;
using KAIROSV2.Business.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace KAIROSV2.Business.Managers
{
    /// <summary>
    /// Maneja la logica de negocio para los Contadores
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para la entidad Contador, las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir Contadores.
    /// </remarks>
    public class ContadoresManager : ManagerBase, IContadoresManager
    {
        private readonly IContadoresRepository _ContadoresRepository;
        private readonly IProductosRepository _ProductosRepository;
        private readonly ILogManager _logManager;

        public ContadoresManager(IContadoresRepository ContadoresRepository, 
            IProductosRepository ProductosRepository, 
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _ContadoresRepository = ContadoresRepository;
            _ProductosRepository = ProductosRepository;
        }

        /// <summary>
        /// Obtiene el Contador incluyendo su imagen
        /// </summary>
        /// <param name="idContador">Id del Contador</param>
        /// <returns>Contador</returns>
        public TContador ObtenerContador(string idContador )
        {
            TContador Contador = new TContador();
            if (_ContadoresRepository.Existe(idContador))
                Contador = _ContadoresRepository.Obtener(idContador );
            return Contador;
        }
                

        public TContador ObtenerContador(string idContador, string[] parametros)
        {
            TContador Contador = new TContador();
            if (_ContadoresRepository.Existe(idContador ))
                Contador = _ContadoresRepository.Obtener(idContador,  parametros);
            return Contador;
        }

        public Task<TContador> ObtenerContadorAsync(string idContador )
        {
            try
            {
                if (_ContadoresRepository.Existe(idContador ))
                    return _ContadoresRepository.ObtenerAsync(idContador );
                else
                    return null;
            }
            catch (Exception e)
            {
                //_logManager.InsertarLog("Admin", "Kairos2", "Operaciones", "Contadores", "", "T_Contadores", LogAcciones.Insertar, "Contador " + idContador + "NO Creada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return null;
            }

        }

        /// <summary>
        /// Obtiene todos los Contadores del sistema incluyendo la imagen de cada uno
        /// </summary>
        /// <returns>Contadores del sistema</returns>
        public Task<IEnumerable<TContador>> ObtenerContadoresAsync()
        {
            return _ContadoresRepository.ObtenerTodasAsync();
        }


        public IEnumerable<TProducto> ObtenerProductos()
        {
            return _ProductosRepository.ObtenerProductos();
        }

        public IEnumerable<TContador> ObtenerContadores(params string[] includes)
        {
            return _ContadoresRepository.ObtenerTodas(includes);
        }        

        public IEnumerable<TContador> ObtenerContadores()
        {
            return _ContadoresRepository.ObtenerTodas();
        }

        //public IEnumerable<TContadoresTipo> ObtenerTiposContador()
        //{
        //    return _ContadoresRepository.ObtenerTipos();
        //}

        //public IEnumerable<TContadoresEstados> ObtenerEstadosContador()
        //{
        //    return _ContadoresRepository.ObtenerEstados();
        //}

        /// <summary>
        /// Crean el Contador en el sistema
        /// </summary>
        /// <param name="Contador">Entidad Contador para crear</param>
        /// <returns>True si creo el Contador, Flase si el Contador ya existe</returns>
        public bool CrearContador(TContador Contador)
        {
            try
            {
                if (_ContadoresRepository.Existe(Contador.IdContador ))
                    return false;
                else
                {
                    _ContadoresRepository.Add(Contador);
                    LogInformacion(LogAcciones.Insertar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", $"Contador {Contador?.IdContador } creado.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Insertar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", $"Contador {Contador?.IdContador } no creado.", e);
                return false;
            }

            return true;
        }

        public async Task<bool> CrearContadorAsync(TContador Contador)
        {
            try
            {
                if (await _ContadoresRepository.ExisteAsync(Contador.IdContador))
                    return false;
                else
                {
                    _ContadoresRepository.Add(Contador);
                    LogInformacion(LogAcciones.Insertar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", "Contador " + Contador?.IdContador + " creado.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Insertar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", "Contador " + Contador?.IdContador + " no creado.", e);
                return false;
            }

            return true;
        }


        public bool ActualizarContador(TContador Contador)
        {
            try
            {
                if (!_ContadoresRepository.Existe(Contador.IdContador ))
                    return false;
                else
                {
                    _ContadoresRepository.Update(Contador);
                    LogInformacion(LogAcciones.Actualizar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", "Contador " + Contador?.IdContador + " actualizado.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", "Contador " + Contador?.IdContador + " no actualizado.", e);
                return false;
            }

            return true;
        }       


        public async Task<bool> ActualizarContadorAsync(TContador Contador)
        {

            try
            {
                if (!await _ContadoresRepository.ExisteAsync(Contador.IdContador ))
                    return false;
                else
                {
                    var DatosAntiguos = _logManager.SerializarEntidad(_ContadoresRepository.Obtener(Contador.IdContador));
                    var DatosNuevos = _logManager.SerializarEntidad(Contador);
                    _ContadoresRepository.Update(Contador);
                    LogInformacion(LogAcciones.Actualizar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", "Contador " + Contador?.IdContador + " actualizado.");
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Actualizar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", "Contador " + Contador?.IdContador + " no actualizado.", e);
                return false;
            }


            return true;
        }

        public bool BorrarContador(string idContador )
        {
            var result = false;
            try
            {
                if (_ContadoresRepository.Existe(idContador ))
                {
                    _ContadoresRepository.Remove(idContador);
                    LogInformacion(LogAcciones.Eliminar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", "Contador " + idContador + " eliminado.");
                    result = true;
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Eliminar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", "Contador " + idContador + " no eliminado.", e);
                return false;
            }

            return result;
        }

        public async Task<bool> BorrarContadorAsync(string idContador )
        {
            var result = false;

            try
            {
                if (await _ContadoresRepository.ExisteAsync(idContador ))
                {
                    _ContadoresRepository.Remove(idContador);
                    LogInformacion(LogAcciones.Eliminar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", "Contador " + idContador + " eliminado.");
                    result = true;
                }
            }
            catch (Exception e)
            {
                LogError(LogAcciones.Eliminar, "Almacenamiento", "Contadores", "Contadores", "T_Contadores", "Contador " + idContador + " no eliminado.", e);
                return false;
            }

            return result;
        }
    }

}
