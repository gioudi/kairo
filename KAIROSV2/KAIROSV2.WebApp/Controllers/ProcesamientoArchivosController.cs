using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Managers;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.Models.Enums;
using KAIROSV2.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Controllers
{
    [Authorize]
    public class ProcesamientoArchivosController : Controller
    {
        private readonly IProcesamientoArchivosMstManager _PaMstManager;
        private readonly IProcesamientoArchivosDetManager _PaDetManager;

        public ProcesamientoArchivosController(IProcesamientoArchivosMstManager PaMstManager, IProcesamientoArchivosDetManager PaDetManager)
        {
            _PaMstManager = PaMstManager;
            _PaDetManager = PaDetManager;
        }

        public IActionResult Index()
        {
            IEnumerable<TProcesamientoArchivosMst> ProcesamientoArchivosMsts;
            ProcesamientoArchivosMsts = _PaMstManager.ObtenerPAMst();

            List<ProcesamientoArchivosViewModel> response = new List<ProcesamientoArchivosViewModel>();

            foreach (TProcesamientoArchivosMst procesamientoArchivo in ProcesamientoArchivosMsts)
            {
                ProcesamientoArchivosViewModel ProcesamientoVm = new ProcesamientoArchivosViewModel()
                {
                    IdMapeo = procesamientoArchivo.IdMapeo,
                    Descripcion = procesamientoArchivo.Descripcion,
                    NumeroTablas = GetNumeroTablas(procesamientoArchivo.IdMapeo),
                    Fecha = procesamientoArchivo.UltimaEdicion,
                    Usuario = procesamientoArchivo.EditadoPor
                };
                response.Add(ProcesamientoVm);
            }
            return View(response);
        }

        [HttpPost]
        public IActionResult ObtenerListado([FromBody] DatosFiltroPeticion datosFiltro)
        {
            IEnumerable<TProcesamientoArchivosMst> ProcesamientoArchivosMsts;
            ProcesamientoArchivosMsts = _PaMstManager.ObtenerPAMst();
            List<ProcesamientoArchivosViewModel> response = new List<ProcesamientoArchivosViewModel>();

            if (datosFiltro != null && !(datosFiltro?.Filtro?.ToLower() == "todos"))
            {
                if (datosFiltro.Filtro.ToLower() == "descripción")
                    ProcesamientoArchivosMsts = ProcesamientoArchivosMsts.Where(e => e.Descripcion.ToLower().Contains(datosFiltro.Buscar.ToLower()));
                if (datosFiltro.Filtro.ToLower() == "título")
                    ProcesamientoArchivosMsts = ProcesamientoArchivosMsts.Where(e => e.IdMapeo.ToLower().Contains(datosFiltro.Buscar.ToLower()));
            }

            foreach (TProcesamientoArchivosMst procesamientoArchivo in ProcesamientoArchivosMsts)
            {
                ProcesamientoArchivosViewModel ProcesamientoVm = new ProcesamientoArchivosViewModel()
                {
                    IdMapeo = procesamientoArchivo.IdMapeo,
                    Descripcion = procesamientoArchivo.Descripcion,
                    NumeroTablas = GetNumeroTablas(procesamientoArchivo.IdMapeo),
                    Fecha = procesamientoArchivo.UltimaEdicion,
                    Usuario = procesamientoArchivo.EditadoPor
                };
                response.Add(ProcesamientoVm);
            }

            return PartialView("_ListaMapeos", response);
        }

        [HttpPost]
        public IActionResult NuevoProcesamientoArchivos()
        {
            var viewModel = new GestionProcesamientoArchivosViewModel()
            {
                Titulo = "Nuevo Mapeo",
                Accion = "Crear",
                MapeoArchivoPaso1 = new MapeoArchivoDatosIniciales() { Nuevo = true },
                Paso1 = true
            };

            return PartialView("_GestionProcesamientoArchivos", viewModel);
        }

        [HttpPost]
        public IActionResult DatosProcesamientoArchivos([FromBody] DatosConsultaPeticion datosMapeo)
        {
            var Mapeo = _PaMstManager.ObtenerPAMst(datosMapeo.IdEntidad);

            _PaMstManager.GetTablasSistema(out List<string> nombresTablas);

            List<string> ColumnasArchivo = _PaDetManager.GetColumnasArchivo(datosMapeo.IdEntidad);

            List<TablasSistema> tablasSistema = GetTablasMapeo(datosMapeo.IdEntidad);

            var viewModel = new GestionProcesamientoArchivosViewModel()
            {
                Titulo = (datosMapeo.Lectura) ? "Detalle Mapeo" : "Editar Mapeo",
                Accion = (datosMapeo.Lectura) ? "" : "Actualizar",
                MapeoArchivoPaso1 = new MapeoArchivoDatosIniciales()
                {
                    Lectura = datosMapeo.Lectura,
                    Descripcion = Mapeo.Descripcion,
                    IdMapeo = Mapeo.IdMapeo,
                    Separador = (SeparadorArchivoEnum)Mapeo.Separador
                },
                MapeoArchivoPaso3 = new MapeoArchivoTablas()
                {
                    Separador = (SeparadorArchivoEnum)Mapeo.Separador,
                    IdMapeo = Mapeo.IdMapeo,
                    Descripcion = Mapeo.Descripcion,
                    Nuevo = false,
                    TablasSistemaListado = nombresTablas,
                    ColumnasArchivo = ColumnasArchivo,
                    TablasSistemas = tablasSistema

                },

                Paso1 = true,
                Paso3 = true
            };
            return PartialView("_GestionProcesamientoArchivos", viewModel);
        }

        private List<TablasSistema> GetTablasMapeo(string idEntidad)
        {

            List<TablasSistema> tablasSistemaRes = new List<TablasSistema>();
            var tablasSistemaNombre = _PaDetManager.ObtenerTablasSistemaPAByKey(idEntidad);
            foreach (string tablaSistemaNombre in (List<string>)tablasSistemaNombre)
            {
                List<TProcesamientoArchivosDet> mapeoTablasSis = (List<TProcesamientoArchivosDet>)_PaDetManager.ObtenerPADetByKeyTabla(idEntidad, tablaSistemaNombre);
                if (mapeoTablasSis.Count > 0)
                {

                    TablasSistema tablasSistema = new TablasSistema()
                    {
                        NombreTabla = mapeoTablasSis[0].IdTabla,
                        Prioridad = mapeoTablasSis[0].Prioridad,
                        Columnas = new List<TablaSistemaColumna>(),
                        NombresColumnas = new List<string>(),
                    };
                    _PaMstManager.GetColumnasTablaSistema(mapeoTablasSis[0].IdTabla, out List<VDbColumna> columnasSis);
                    foreach (VDbColumna columna in columnasSis)
                    {
                        tablasSistema.NombresColumnas.Add(columna.Name);
                    }
                    foreach (TProcesamientoArchivosDet PaDet in mapeoTablasSis)
                    {
                        VDbColumna columnasis = columnasSis.Find(c => c.Name == PaDet.IdCampo);
                        TablaSistemaColumna PaDetCol = new TablaSistemaColumna()
                        {
                            ColumnaArchivo = PaDet.IdColumna,
                            IndexColumnaArchivo = PaDet.IndiceColumna,
                            IsNull = (bool)columnasis.IsNullable,
                            Llave = columnasis.IsIdentity,
                            Longitud = columnasis.MaxLength,
                            Nombre = PaDet.IdCampo
                        };
                        tablasSistema.Columnas.Add(PaDetCol);
                    }
                    tablasSistemaRes.Add(tablasSistema);

                }

            }
            return tablasSistemaRes;
        }

        [HttpPost]
        public async Task<IActionResult> BorrarProcesamientoArchivos([FromBody] string idMapeo)
        {
            var response = new MessageResponse();
            var Mapeo = await _PaMstManager.ObtenerPAMstAsync(idMapeo);

            if (Mapeo != null)
            {
                try
                {
                    _PaDetManager.BorrarPaDet(idMapeo);
                    response.Result = await _PaMstManager.BorrarPaMstAsync(idMapeo);
                    response.Message = "Mapeo borrado correctamente";
                }
                catch (Exception)
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error no es popsible borrar el Maeo";
                }
            }
            else
            {
                response.Message = "No se encontro el Area que desea borrar";
            }


            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearProcesamientoArchivos([FromForm] MapeoArchivoTablas addPaMstViewModel)
        {
            var response = new MessageResponse();


            if (ModelState.IsValid)
            {
                try
                {
                    response.Result = _PaMstManager.CrearPAMst(addPaMstViewModel.ExtraerMapeo(User.Claims.First().Value));
                    if (response.Result)
                    {
                        try
                        {
                            if (addPaMstViewModel != null)
                            {
                                foreach (TablasSistema tabla in addPaMstViewModel.TablasSistemas)
                                {
                                    foreach (TablaSistemaColumna columna in tabla.Columnas)
                                    {
                                        TProcesamientoArchivosDet TPaDet = new TProcesamientoArchivosDet()
                                        {
                                            IdMapeo = addPaMstViewModel.IdMapeo,
                                            IdColumna = columna.ColumnaArchivo,
                                            IndiceColumna = columna.IndexColumnaArchivo,
                                            IdCampo = columna.Nombre,
                                            Habilitado = true,
                                            Prioridad = tabla.Prioridad,
                                            IdTabla = tabla.NombreTabla,
                                            EditadoPor = User.Claims.First().Value,
                                            UltimaEdicion = DateTime.Now
                                        };

                                        _PaDetManager.CrearPADet(TPaDet);
                                    }

                                }
                            }


                            response.Message = "Mapeo creado correctamente";
                        }
                        catch (Exception ex)
                        {
                            response.Message = "Mapeo creado, hubo problemas";
                        }
                    }

                    else
                        response.Message = "El Mapeo ya existe";
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible crear el Mapeo";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Falta completar algun dato";
            }

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarProcesamientoArchivos([FromForm] MapeoArchivoTablas updateMapeoViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    TProcesamientoArchivosMst mapeo = updateMapeoViewModel.ExtraerMapeo(User.Claims.First().Value);
                    response.Result = _PaMstManager.ActualizarPaMst(mapeo);
                    if (response.Result)
                    {
                        try
                        {
                            int TablasActualizar = (from p in updateMapeoViewModel.TablasSistemas select p.NombreTabla).ToList().Distinct().Count();
                            if (TablasActualizar
                                != (from p in updateMapeoViewModel.TablasSistemas select p.NombreTabla).ToList().Count()) //Hay Tablas Repetidas
                            {
                                response.Result = false;
                                response.Message = "No es posible realizar el mapeo la Tabla ya existe";
                                return Json(response);
                            }

                            List<string> tablasActual = _PaDetManager.ObtenerTablasSistemaPAByKey(mapeo.IdMapeo).ToList();
                            foreach (string tablaActual in tablasActual)
                            {
                                if (!updateMapeoViewModel.TablasSistemas.Any(e => e.NombreTabla == tablaActual))
                                {
                                    _PaDetManager.BorrarPaDetByKey(mapeo.IdMapeo, tablaActual);
                                }
                            }
                            foreach (TablasSistema tabla in updateMapeoViewModel.TablasSistemas)
                            {


                                foreach (TablaSistemaColumna columna in tabla.Columnas)
                                {
                                    TProcesamientoArchivosDet TPaDet = new TProcesamientoArchivosDet()
                                    {
                                        IdMapeo = updateMapeoViewModel.IdMapeo,
                                        IdColumna = columna.ColumnaArchivo,
                                        IndiceColumna = columna.IndexColumnaArchivo,
                                        IdCampo = columna.Nombre,
                                        Habilitado = true,
                                        Prioridad = tabla.Prioridad,
                                        IdTabla = tabla.NombreTabla,
                                        EditadoPor = User.Claims.First().Value,
                                        UltimaEdicion = DateTime.Now

                                    };
                                    _PaDetManager.ActualizarPaDet(TPaDet);
                                }
                            }
                            response.Message = "Mapeo actualizado con éxito";
                        }
                        catch (Exception)
                        {
                            response.Message = "Mapeo Actuualizado, fallo la actualizacion datos de detalle";
                        }
                    }
                    else
                        response.Message = "El Mapeo no existe";
                }
                catch (Exception)
                {
                    response.Result = false;
                    response.Message = "No fue posible actualizar el mapeo";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible actualizar el mapeo";
            }

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PrevisualizarArchivo([FromForm] MapeoArchivoDatosIniciales MapeoViewModel)
        {
            var response = new MessageResponse();

            var previsualizacionArchivo = new MapeoArchivoPrevisualizacion();



            if (ModelState.IsValid)
            {
                if (_PaMstManager.Existe(MapeoViewModel.IdMapeo))
                {
                    response.Result = false;
                    response.Message = "Mapeo ya existe";
                }
                else
                {
                    try
                    {
                        List<string> encabezadosArchivo = new List<string>();
                        List<List<string>> muestraaDatosArchivo = new List<List<string>>();

                        char separador;
                        if (MapeoViewModel.Separador == SeparadorArchivoEnum.Otro)
                        {
                            separador = char.Parse(MapeoViewModel.OtroCaracter);
                        }
                        else
                        {
                            separador = (char)MapeoViewModel.Separador;
                        }

                        response.Result = _PaMstManager.ProcesarArchivoTexto(MapeoViewModel.Archivo, separador, out encabezadosArchivo, out muestraaDatosArchivo);
                        if (response.Result)
                        {

                            previsualizacionArchivo.ColumnasArchivo = encabezadosArchivo;
                            previsualizacionArchivo.MuestraDatosArchivo = muestraaDatosArchivo;
                            response.Payload = previsualizacionArchivo;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "No contiene el separador indicado";
                        }

                    }
                    catch (Exception ex)
                    {
                        response.Result = false;
                        response.Message = "Hubo Un Problema Al procesar el Archivo";
                    }
                }
            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible procesar el archivo";
            }


            ViewData["Result"] = response.Result;
            ViewData["Message"] = response.Message;
            return PartialView("_Paso2Previsualizacion", previsualizacionArchivo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcesarArchivo([FromForm] MapeoArchivoDatosIniciales MapeoViewModel)
        {
            var response = new MessageResponse();

            var previsualizacionArchivo = new MapeoArchivoPrevisualizacion();

            if (ModelState.IsValid)
            {
                try
                {
                    char separador;
                    if (MapeoViewModel.Separador == SeparadorArchivoEnum.Otro)
                    {
                        separador = char.Parse(MapeoViewModel.OtroCaracter);
                    }
                    else
                    {
                        separador = (char)MapeoViewModel.Separador;
                    }

                    response.Result = _PaDetManager.ProcesarArchivoTextoToBD(MapeoViewModel.Archivo, separador, MapeoViewModel.IdMapeo);
                    if (response.Result)
                    {
                        response.Result = true;
                        response.Message = "Archivo Procesado Con Éxito";

                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "No contiene el separador indicado";
                    }

                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "Hubo Un Problema Al procesar el Archivo";
                }

            }
            else
            {
                response.Result = false;
                response.Message = "No fue posible procesar el archivo";
            }
            ViewData["Result"] = response.Result;
            ViewData["Message"] = response.Message;
            return PartialView("_Paso2Previsualizacion", previsualizacionArchivo);
        }




        [HttpPost]
        public IActionResult NuevoMapeoArchivoTablas()
        {
            var vm = new MapeoArchivoTablas()
            {
                Lectura = false,
                Nuevo = true,
                TablasSistemaListado = new List<string>() { "Tabla 1", }
            };

            return PartialView("_Paso3Mapeo", vm);
        }

        [HttpPost]
        public IActionResult GetTablaColumnas([FromBody] string nombreTabla)
        {
            var response = new MessageResponse();
            TablasSistema tablasSistema = new TablasSistema();


            if (ModelState.IsValid)
            {
                try
                {
                    response.Result = _PaMstManager.GetColumnasTablaSistema(nombreTabla, out List<VDbColumna> columnasSis);
                    if (response.Result)
                    {
                        tablasSistema.NombreTabla = nombreTabla;
                        tablasSistema.Columnas = new List<TablaSistemaColumna>();
                        tablasSistema.NombresColumnas = new List<string>();
                        foreach (VDbColumna columna in columnasSis)
                        {
                            TablaSistemaColumna col = new TablaSistemaColumna()
                            {
                                Nombre = columna.Name,
                                Llave = columna.IsIdentity,
                                IsNull = (bool)columna.IsNullable,
                                Tipo = columna.Tipo,
                                Longitud = columna.MaxLength
                            };
                            // tablasSistema.NombresColumnas.Add(columna.Name);
                            tablasSistema.Columnas.Add(col);
                        }

                        response.Payload = tablasSistema;
                    }

                }
                catch (Exception)
                {
                    response.Result = false;
                    response.Message = "No Fue Posible traer la infromación del sistema";
                }

            }
            else
            {
                response.Result = false;
                response.Message = "No Fue Posible traer la infromación del sistema";
            }

            return Json(response.Payload);
        }

        public IActionResult GetTablas([FromForm] GestionProcesamientoArchivosViewModel MapeoViewModel)
        {
            var response = new MessageResponse();
            List<string> nombresTablas = new List<string>();
            try
            {

                response.Result = _PaMstManager.GetTablasSistema(out nombresTablas);
                if (response.Result)
                {

                    response.Payload = nombresTablas;
                }

            }
            catch (Exception)
            {
                response.Result = false;
                response.Message = "Hubo Un Problema Al procesar el Archivo";
            }


            return Json(nombresTablas);

        }

        private int GetNumeroTablas(string idMapeo)
        {
            return _PaDetManager.ObtenerTablasSistemaPAByKey(idMapeo).ToList().Count();
        }

    }
}
