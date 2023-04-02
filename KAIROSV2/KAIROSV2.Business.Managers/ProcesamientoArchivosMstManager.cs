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
using System.IO;
using Microsoft.VisualBasic.FileIO;
using KAIROSV2.Business.Entities.Enums;

namespace KAIROSV2.Business.Managers
{
    /// <summary>
    /// Maneja la logica de negocio para los el procesamiento de archivos    /// 
    /// </summary>
    /// <remarks>
    /// Realiza las validaciones necesarias para la entidad area, las reglas de negocio 
    /// y la interaccion con la base de datos para presentar y persistir areas.
    /// </remarks>
    public class ProcesamientoArchivosMstManager : IProcesamientoArchivosMstManager
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly IProcesamientoArchivosMstRepository _PARepository;

        private readonly IColumnasSistemaRepository _ColumnasSistemaRepository;

        private readonly ITablasSistemaRepository _TablasSistemaRepository;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogManager _logManager;

        #endregion



        #region Constructor

        public ProcesamientoArchivosMstManager(IProcesamientoArchivosMstRepository PAMstRepository,
            IColumnasSistemaRepository ColumnasSistemaRepository, ITablasSistemaRepository TablasSistemaRepository, ILogManager logManager)
        {
            _PARepository = PAMstRepository;
            _logManager = logManager;
            _ColumnasSistemaRepository = ColumnasSistemaRepository;
            _TablasSistemaRepository = TablasSistemaRepository;

        }

        #endregion


        /// <summary>
        /// Obtiene el area incluyendo su imagen
        /// </summary>
        /// <param name="idarea">Id del area</param>
        /// <returns>area</returns>
        public TProcesamientoArchivosMst ObtenerPAMst(string idMapeo)
        {
            return _PARepository.Obtener(idMapeo);
        }

        public Task<TProcesamientoArchivosMst> ObtenerPAMstAsync(string idMapeo)
        {
            return _PARepository.ObtenerAsync(idMapeo);
        }

        /// <summary>
        /// Obtiene todos los areas del sistema incluyendo la imagen de cada uno
        /// </summary>
        /// <returns>areas del sistema</returns>
        public IEnumerable<TProcesamientoArchivosMst> ObtenerPAMst()
        {
            return _PARepository.ObtenerTodas();
        }




        /// <summary>
        /// Crean el area en el sistema
        /// </summary>
        /// <param name="area">Entidad area para crear</param>
        /// <returns>True si creo el area, Flase si el area ya existe</returns>
        public bool CrearPAMst(TProcesamientoArchivosMst paMst)
        {
            if (_PARepository.Existe(paMst.IdMapeo))
                return false;
            else
            {
                _PARepository.Add(paMst);
                _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos Mestro", "", "T_Procesamiento_Archivos", LogAcciones.Insertar, "Procesamiento Archivos" + paMst.IdMapeo + " Agregada", LogPrioridades.Informacion);

            }

            return true;
        }

        /// <summary>
        /// Actualiza los datos del area
        /// </summary>
        /// <param name="area">Entidad area para actualizar</param>
        /// <returns>True si se actualizo el area, False si no existe el area</returns>
        public bool ActualizarPaMst(TProcesamientoArchivosMst paMst)
        {
            try
            {
                if (!_PARepository.Existe(paMst.IdMapeo))
                    return false;
                else
                {
                    _PARepository.Update(paMst);
                    _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos Mestro", "", "T_Procesamiento_Archivos", LogAcciones.Insertar, "Procesamiento Archivos" + paMst.IdMapeo + " Actualizada", LogPrioridades.Informacion);
                }

            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos", "", "T_Procesamiento_Archivos_Mst", LogAcciones.Actualizar, "Procesamiento " + paMst.IdMapeo + " NO Actualizada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return true;
        }

        public bool BorrarPaMst(string idMapeo)
        {
            var result = false;
            try
            {
                if (_PARepository.Existe(idMapeo))
                {
                    _PARepository.Remove(idMapeo);
                    _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos Mestro", "", "T_Procesamiento Archivos", LogAcciones.Insertar, "Procesamiento Archivos" + idMapeo + " Eliminada", LogPrioridades.Informacion);
                    result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos", "", "T_Procesamiento_Archivos_Mst", LogAcciones.Actualizar, "Procesamiento " + idMapeo + " NO Eliminada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                result = false;
            }

            return result;
        }

        public async Task<bool> BorrarPaMstAsync(string idMapeo)
        {
            var result = false;

            try
            {
                if (_PARepository.Existe(idMapeo))
                {
                    _PARepository.Remove(idMapeo);
                    _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos Mestro", "", "T_Procesamiento Archivos", LogAcciones.Insertar, "Procesamiento Archivos" + idMapeo + " Eliminada", LogPrioridades.Informacion);
                    result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos", "", "T_Procesamiento_Archivos_Mst", LogAcciones.Actualizar, "Procesamiento " + idMapeo + " NO Eliminada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                result = false;
            }

            return result;
        }


        public bool Existe(string idMapeo)
        {
            if (_PARepository.Existe(idMapeo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        public bool GetColumnasTablaSistema(string currTabla, out List<VDbColumna> columnasSis)
        {

            try
            {
                int tablaId = _TablasSistemaRepository.Obtener(currTabla).ObjectId;
                columnasSis = _ColumnasSistemaRepository.ObtenerColumnasTabla(tablaId).ToList();
                return true;
            }
            catch (Exception e)
            {
                columnasSis = null;
                return false;
            }
        }

        public bool GetTablasSistema(out List<string> tablasSis)
        {
            try
            {
                tablasSis = _TablasSistemaRepository.ObtenerNombresTablas().ToList();

                return true;
            }
            catch
            {
                tablasSis = null;
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="separador"></param>
        /// <param name="encabezadosColumnas"></param>
        /// <param name="muestraData"></param>
        /// <returns></returns>
        public bool ProcesarArchivoTexto(IFormFile archivo, char separador, out List<string> encabezadosColumnas, out List<List<string>> muestraData)
        {
            string[] arrSeparador = { separador.ToString() };

            muestraData = new List<List<string>>();
            encabezadosColumnas = new List<string>();


            using (Stream ms = archivo.OpenReadStream())
            {
                //Reads the first line and validate the existence of the separator
                string line = string.Empty;
                StreamReader sr = new StreamReader(ms);
                line = sr.ReadLine();
                if (line.Contains(separador))
                {
                    ms.Position = 0;
                    using (TextFieldParser reader = new(ms))
                    {
                        reader.Delimiters = arrSeparador;

                        string[] currRow;
                        int indx = 0;
                        while (!reader.EndOfData)
                        {
                            try
                            {
                                currRow = reader.ReadFields();
                                if (indx == 0) //encabezado
                                {
                                    encabezadosColumnas = currRow.ToList<string>();
                                }
                                else
                                {
                                    muestraData.Add(currRow.ToList<string>());
                                }
                            }
                            catch
                            {
                                throw;
                            }
                            indx++;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


    }

}
