using AutoMapper;
using KAIROSV2.Business.Common.Exceptions;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.WebApp.Identity.Authorization;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Controllers
{
    [Authorize]
    [PermissionsAuthorize(Permissions.SubModuloProductos)]
    public class ProductosController : BaseController
    {
        private const string Vista = "Productos";
        private const string VistaGestion = "Gestión producto";
        private const string TablaProductos = "T_Productos";
        private const string TablaTerminalesProductosRecetas = "T_Terminales_Productos_Recetas";
        private readonly IProductosManager _productosManager;
        private readonly IAuthorizationService _authorization;

        public ProductosController(IProductosManager productosManager)
        {
            Area = "Operaciones";
            _productosManager = productosManager;
        }

        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, TablaProductos, $"Ingreso a vista {Vista}");

            return View(new ListViewModel<TProducto>
            {
                Encabezados = new List<string>() { "Icono", "Código Producto", "Nombre Corto", "Estado", "Tipo", "Clase", "SICOM", "Acciones" },
                Entidades = _productosManager.ObtenerProductos()?.OrderBy(e => e.NombreCorto),
                ActionsPermission = new ActionsPermission(User, Permissions.ProductosAccionCN, Permissions.ProductosAccionB, Permissions.ProductosAccionE, Permissions.ProductosAccionVD, Permissions.None, Permissions.None)
            });
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ProductosAccionCN)]
        public IActionResult NuevoProducto()
        {
            var tipos = _productosManager.ObtenerTiposProducto();
            var clases = _productosManager.ObtenerClasesProducto();

            var viewModel = new GestionProductosViewModel()
            {
                Titulo = "Nuevo Producto",
                Accion = "Crear",
                Estado = true,
                TipoProducto = new List<SelectListItem>(),
                ClaseProducto = new List<SelectListItem>(),
                Recetas = new List<RecetaDTO>()
            };

            tipos?.ToList().ForEach(e => viewModel.TipoProducto.Add(new SelectListItem() { Text = e.Descripcion, Value = e.IdTipo }));
            clases?.ToList().ForEach(e => viewModel.ClaseProducto.Add(new SelectListItem() { Text = e.Descripcion, Value = e.IdClase }));

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingreso a vista {VistaGestion}");
            return PartialView("_GestionProducto", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ProductosAccionB)]
        public async Task<IActionResult> BorrarProducto([FromBody] string idProducto)
        {
            var response = new MessageResponse();

            try
            {
                response.Result = _productosManager.BorrarProducto(idProducto);
                if (response.Result)
                    response.Message = "Producto eliminado correctamente";
                else
                    response.Message = "No se encontró el producto que desea eliminar";

                LogInformacion(LogAcciones.Eliminar, Vista, TablaProductos, $"Producto {idProducto}. {response?.Message}");
            }
            catch (DeleteCascadeException ex)
            {
                response.Message = ex.Message;
                LogError(LogAcciones.Eliminar, Vista, TablaProductos, $"Producto {idProducto} no eliminado.", ex);
            }
            catch (Exception ex)
            {
                response.Message = "Ocurrió un error no es posible eliminar el Producto";
                LogError(LogAcciones.Eliminar, Vista, TablaProductos, $"Producto {idProducto} no eliminado.", ex);
            }

            return Json(response);
        }

        [HttpPost]
        public IActionResult DatosProducto([FromBody] DatosConsultaPeticion datosProducto)
        {
            var producto = _productosManager.ObtenerProductoConRecetas(datosProducto.IdEntidad);
            var viewModel = Mapper.Map<GestionProductosViewModel>(producto);
            var tipos = _productosManager.ObtenerTiposProducto();
            var clases = _productosManager.ObtenerClasesProducto();
            var productos = _productosManager.ObtenerProductosPorClase(producto.IdClase);
            viewModel.EstablecerAtributos(producto.TProductosAtributos?.ToList());
            viewModel.TipoProducto = new List<SelectListItem>(tipos?.Count() ?? 0);
            viewModel.ClaseProducto = new List<SelectListItem>(clases?.Count() ?? 0);
            viewModel.ProductosComponentes = new List<SelectListItem>(productos?.Count() ?? 0);
            viewModel.Lectura = datosProducto.Lectura;

            productos?.ToList().ForEach(e => viewModel.ProductosComponentes.Add(new SelectListItem() { Text = e.NombreCorto, Value = e.IdProducto }));
            tipos?.ToList().ForEach(e => viewModel.TipoProducto.Add(new SelectListItem() { Text = e.Descripcion, Value = e.IdTipo }));
            clases?.ToList().ForEach(e => viewModel.ClaseProducto.Add(new SelectListItem() { Text = e.Descripcion, Value = e.IdClase }));
            viewModel.Titulo = (datosProducto.Lectura) ? "Detalle Producto" : "Editar Producto";
            viewModel.Accion = (datosProducto.Lectura) ? "" : "Actualizar";

            LogInformacion(LogAcciones.IngresoVista, VistaGestion, "", $"Ingresó a la vista {VistaGestion}");
            return PartialView("_GestionProducto", viewModel);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ProductosAccionCN)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearProducto([FromForm] GestionProductosViewModel addProductoViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    var producto = Mapper.Map<TProducto>(addProductoViewModel);
                    producto.TProductosAtributos = addProductoViewModel.ExtraerAtributos();
                    var creationResponse = _productosManager.CrearProducto(producto);
                    response.Result = creationResponse.Result;
                    if (response.Result)
                        response.Message = "Producto creado correctamente";
                    else
                        response.Message = creationResponse.GetFailuresToString();

                    LogInformacion(LogAcciones.Insertar, VistaGestion, TablaProductos, $"Producto {producto?.IdProducto}. {response?.Message}");
                }
                catch (Exception ex)
                {
                    response.Message = "Ocurrió un error creando el producto.";
                    LogError(LogAcciones.Insertar, VistaGestion, TablaProductos, response?.Message, ex);
                }
            }
            else
            {
                response.Result = false;
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                response.Message = allErrors.FirstOrDefault().ErrorMessage;
                LogInformacion(LogAcciones.Insertar, VistaGestion, TablaProductos, $"No fue posible crear producto {addProductoViewModel?.IdProducto}. {response?.Message}");
            }

            return Json(response);
        }

        [HttpPost]
        [PermissionsAuthorize(Permissions.ProductosAccionE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarProducto([FromForm] GestionProductosViewModel updateProductoViewModel)
        {
            var response = new MessageResponse();

            if (ModelState.IsValid)
            {
                try
                {
                    var anteriorProducto = _productosManager.ObtenerProducto(updateProductoViewModel.IdProducto);
                    var producto = Mapper.Map<TProducto>(updateProductoViewModel);
                    producto.TProductosReceta = null;
                    producto.TProductosAtributos = null;
                    var updateResponse = _productosManager.ActualizarProducto(producto);
                    response.Result = updateResponse.Result;
                    if (response.Result)
                    {
                        try
                        {
                            var atributos = _productosManager.AgregarActualizarAtributosProducto(updateProductoViewModel.ExtraerAtributos());
                            var resultRecetas = _productosManager.AgregarActualizarRecetasProductoFormulario(updateProductoViewModel.Recetas, producto.IdProducto);
                            response.Result = atributos && resultRecetas.Result;

                            response.Message = "Producto actualizado" +
                           (atributos ? "" : ", fallo la actualización de atributos") +
                           (resultRecetas.Result ? "" : $", {resultRecetas.GetFailuresToString()}");

                            LogInformacionActualizar(VistaGestion, TablaProductos, $"Producto {updateProductoViewModel.IdProducto}. {response.Message}", anteriorProducto, producto);
                        }
                        catch (Exception ex)
                        {
                            response.Result = false;
                            response.Message = "Producto actualizado, ocurrió un error persistiendo las recetas";
                            LogError(LogAcciones.Actualizar, VistaGestion, TablaTerminalesProductosRecetas, $"Error persistiendo las recetas del producto: {producto?.IdProducto}", ex);
                        }
                    }
                    else
                        response.Message = updateResponse.GetFailuresToString();
                }
                catch (Exception ex)
                {
                    response.Result = false;
                    response.Message = "No fue posible actualizar el producto";
                    LogError(LogAcciones.Actualizar, VistaGestion, TablaProductos, $"Producto {updateProductoViewModel?.IdProducto}, no fue posible actualizar.", ex);
                }
            }
            else
            {
                response.Result = false;
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                response.Message = allErrors.FirstOrDefault().ErrorMessage;
                LogInformacion(LogAcciones.Actualizar, VistaGestion, TablaProductos, $"No fue posible actualizar producto {updateProductoViewModel?.IdProducto}. {response?.Message}");
            }

            return Json(response);
        }

        [HttpPost]
        public IActionResult ComponentesDisponibles([FromBody] string claseId)
        {
            var productos = _productosManager.ObtenerProductosPorClase(claseId);
            return Json(productos?.OrderBy(e => e.NombreCorto)?.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerPermisos()
        {
            var permisos = new ActionsPermission(User, Permissions.ProductosAccionCN, Permissions.ProductosAccionB, Permissions.ProductosAccionE, Permissions.ProductosAccionVD, Permissions.None, Permissions.None);

            return Json(permisos);
        }

        private IActionResult AccionNoPermitida(string titulo, string mensaje)
        {
            ViewData["Titulo"] = titulo;
            ViewData["Mensaje"] = mensaje;
            return PartialView("_AccionNoPermitida");
        }
    }
}
