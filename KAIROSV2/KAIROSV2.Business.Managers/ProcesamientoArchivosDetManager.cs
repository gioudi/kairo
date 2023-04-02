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
    public class ProcesamientoArchivosDetManager : IProcesamientoArchivosDetManager
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly IProcesamientoArchivosDetRepository _PARepository;

        private readonly IColumnasSistemaRepository _ColumnasSistemaRepository;

        private readonly ITablasSistemaRepository _TablasSistemaRepository;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogManager _logManager;

        #endregion



        #region Constructor

        public ProcesamientoArchivosDetManager(IProcesamientoArchivosDetRepository PADetRepository, ILogManager logManager,
            IColumnasSistemaRepository ColumnasSistemaRepository, ITablasSistemaRepository TablasSistemaRepository)
        {
            _PARepository = PADetRepository;
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
        public TProcesamientoArchivosDet ObtenerPADet(string idMapeo)
        {
            return _PARepository.Obtener(idMapeo);
        }

        public Task<TProcesamientoArchivosDet> ObtenerPADetAsync(string idMapeo)
        {
            return _PARepository.ObtenerAsync(idMapeo);
        }

        /// <summary>
        /// Obtiene todos los areas del sistema incluyendo la imagen de cada uno
        /// </summary>
        /// <returns>areas del sistema</returns>
        public IEnumerable<TProcesamientoArchivosDet> ObtenerPADet()
        {
            return _PARepository.ObtenerTodas();
        }


        public IEnumerable<TProcesamientoArchivosDet> ObtenerPADetByKey(string idMapeo)
        {
            return _PARepository.ObtenerByKey(idMapeo);
        }


        public IEnumerable<string> ObtenerTablasSistemaPAByKey(string idMapeo)
        {
            return _PARepository.ObtenerTablasSistemaByKey(idMapeo).ToList();

        }


        public IEnumerable<TProcesamientoArchivosDet> ObtenerPADetByKeyTabla(string idMapeo, string idTabla)
        {
            return _PARepository.ObtenerTablasSistemaByKeyTabla(idMapeo, idTabla);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="separador"></param>
        /// <param name="idMapeo"></param>
        /// <returns></returns>
        public bool ProcesarArchivoTextoToBD(IFormFile archivo, char separador, string idMapeo)
        {

            string[] arrSeparador = { separador.ToString() };

            var tablasSistemaNombre = ObtenerTablasSistemaPAByKey(idMapeo);

            int Counter = 0;
            int CounterTablas = tablasSistemaNombre.Count();
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

                        List<string> encabezado = new List<string>();
                        while (!reader.EndOfData)
                        {
                            try
                            {
                                currRow = reader.ReadFields();
                                if (indx == 0) //encabezado
                                {
                                    encabezado = currRow.ToList<string>();
                                }
                                else
                                {
                                    foreach (string tablaSistemaNombre in (List<string>)tablasSistemaNombre)
                                    {

                                        List<VDbColumna> columnasSis;
                                        int tablaId = _TablasSistemaRepository.Obtener(tablaSistemaNombre).ObjectId;
                                        columnasSis = _ColumnasSistemaRepository.ObtenerColumnasTabla(tablaId).ToList();
                                        List<string> datos = currRow.ToList<string>();
                                        string sql = ProcesarLinea(datos, tablaSistemaNombre, columnasSis, idMapeo, encabezado);
                                        string res = _PARepository.InsertData(sql);
                                        if (res != "Exito")
                                        {
                                            _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "ProcesamientoA Archivos", "", tablaSistemaNombre, LogAcciones.Insertar, "Fallo Insercion: " + res, LogPrioridades.Informacion);
                                        }
                                        else
                                        {
                                            Counter++;
                                        }

                                    }
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
                    _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Procesamiento Archivos", "", "Archivo ", LogAcciones.Insertar, "El Archivo no tiene el separador especificado", LogPrioridades.Informacion);
                    return false;
                }
            }
            _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Procesamiento Archivos", "", "Archivo ", LogAcciones.Insertar, "Se insertaron [" + Counter + "] Registros en [" + CounterTablas + "] Tablas", LogPrioridades.Informacion);
            return true;
        }

        string ProcesarLinea(List<string> datos, string nombreTabla, List<VDbColumna> columnasSis, string idMapeo, List<string> encabezado)
        {
            StringBuilder sbQuery = new StringBuilder();

            StringBuilder sNames = new StringBuilder();
            StringBuilder sValues = new StringBuilder();

            sbQuery.Append("INSERT INTO ");
            sbQuery.Append(nombreTabla);
            int indx = 0;
            foreach (VDbColumna columna in columnasSis)
            {
                string dato = GetValidDato(idMapeo, nombreTabla, datos, columna, encabezado);
                if (!columna.IsIdentity && dato != "nulo")
                {
                    if (indx == 0)
                    {
                        sNames.Append(" (");
                        sNames.Append(columna.Name);
                        sValues.Append(" (");
                        sValues.Append(dato);
                    }
                    else
                    {
                        sNames.Append(",");
                        sNames.Append(columna.Name);
                        sValues.Append(",");
                        sValues.Append(dato);
                    }
                }
                indx++;
            }

            sNames.Append(") ");

            sValues.Append(") ");


            sbQuery.Append(sNames);
            sbQuery.Append("VALUES");
            sbQuery.Append(sValues);

            return sbQuery.ToString();
        }


        string GetValidDato(string idMapeo, string IdTabla, List<string> datos, VDbColumna columna, List<string> encabezado)
        {
            string nColumnaArchivo = _PARepository.NombreColumnaArchivoByKeyTabla(idMapeo, IdTabla, columna.Name);

            int indexDato = encabezado.IndexOf(nColumnaArchivo);
            if (indexDato < datos.Count)
            {
                if (columna.Name == "Editado_por")
                {
                    return "admin";
                }
                else if (columna.Name == "Ultima_Edicion")
                {
                    return DateTime.Now.ToShortDateString();
                }
                return datos[indexDato];
            }
            else
            {
                if (!(bool)columna.IsNullable)
                {
                    switch (columna.Tipo)
                    {
                        case "int":
                        case "tinyint":
                        case "bigint":
                        case "smallint":
                        case "float":
                        case "double":
                            return "0";
                        case "char":
                        case "varchar":
                        case "nvarchar":
                            return " ";
                        case "bit":
                            return "0";
                        case "DateTime":
                            return DateTime.MinValue.ToShortDateString();
                    }
                }
                else
                {
                    return "nulo";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Crean el area en el sistema
        /// </summary>
        /// <param name="area">Entidad area para crear</param>
        /// <returns>True si creo el area, Flase si el area ya existe</returns>
        public bool CrearPADet(TProcesamientoArchivosDet paDet)
        {
            if (_PARepository.Existe(paDet.IdMapeo, paDet.IdTabla, paDet.IdCampo))
                return false;
            else
            {
                _PARepository.Add(paDet);
                _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos Mestro", "", "T_Procesamiento_Archivos", LogAcciones.Insertar, "Procesamiento Archivos" + paDet.IdMapeo + " Agregada", LogPrioridades.Informacion);

            }

            return true;
        }

        /// <summary>
        /// Actualiza los datos del area
        /// </summary>
        /// <param name="area">Entidad area para actualizar</param>
        /// <returns>True si se actualizo el area, False si no existe el area</returns>
        public bool ActualizarPaDet(TProcesamientoArchivosDet paDet)
        {
            try
            {
                if (!_PARepository.Existe(paDet.IdMapeo, paDet.IdTabla, paDet.IdCampo))
                {
                    _PARepository.Add(paDet);
                    _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos Mestro", "", "T_Procesamiento_Archivos", LogAcciones.Insertar, "Procesamiento Archivos" + paDet.IdMapeo + " Agregada", LogPrioridades.Informacion);
                }
                else
                {
                    _PARepository.Update(paDet);
                    _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos Mestro", "", "T_Procesamiento_Archivos", LogAcciones.Insertar, "Procesamiento Archivos" + paDet.IdMapeo + " Actualizada", LogPrioridades.Informacion);
                }

            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos", "", "T_Procesamiento_Archivos_Det", LogAcciones.Actualizar, "Procesamiento " + paDet.IdMapeo + " NO Actualizada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                return false;
            }

            return true;
        }






        public bool BorrarPaDet(string idMapeo)
        {
            var result = false;
            try
            {
                if (_PARepository.Existe(idMapeo))
                {
                    List<TProcesamientoArchivosDet> paDets = _PARepository.ObtenerByKey(idMapeo);
                    foreach (TProcesamientoArchivosDet paDet in paDets)
                    {
                        _PARepository.Remove(idMapeo);
                    }
                    _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos Mestro", "", "T_Procesamiento Archivos", LogAcciones.Insertar, "Procesamiento Archivos" + idMapeo + " Eliminada", LogPrioridades.Informacion);
                    result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos", "", "T_Procesamiento_Archivos_Det", LogAcciones.Actualizar, "Procesamiento " + idMapeo + " NO Eliminada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                result = false;
            }

            return result;
        }

        public List<string> GetColumnasArchivo(string idMapeo)
        {
            return _PARepository.ObtenerColumnasArchivoByKey(idMapeo);
        }


        public async Task<bool> BorrarPaDetAsync(string idMapeo)
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
                _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos", "", "T_Procesamiento_Archivos_Det", LogAcciones.Actualizar, "Procesamiento " + idMapeo + " NO Eliminada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                result = false;
            }

            return result;
        }

        public bool BorrarPaDetByKey(string idMapeo, string tablaActual)
        {
            var result = false;
            try
            {
                if (_PARepository.Existe(idMapeo))
                {
                    List<TProcesamientoArchivosDet> paDets = _PARepository.ObtenerTablasSistemaByKeyTabla(idMapeo, tablaActual).ToList();
                    foreach (TProcesamientoArchivosDet paDet in paDets)
                    {
                        _PARepository.Remove(paDet);
                    }
                    _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos Mestro", "", "T_Procesamiento Archivos", LogAcciones.Insertar, "Procesamiento Archivos" + idMapeo + " Eliminada", LogPrioridades.Informacion);
                    result = true;
                }
            }
            catch (Exception e)
            {
                _logManager.InsertarLog("Admin", "Kairos2", "Procesos", "Archivos", "", "T_Procesamiento_Archivos_Det", LogAcciones.Actualizar, "Procesamiento " + idMapeo + " NO Eliminada. Error: " + _logManager.ManejoErrores(e), LogPrioridades.Error);
                result = false;
            }

            return result;
        }
    }

}
