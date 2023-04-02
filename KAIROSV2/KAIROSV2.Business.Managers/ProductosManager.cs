using AutoMapper;
using KAIROSV2.Business.Common.Exceptions;
using KAIROSV2.Business.Contracts.Engines;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.Data.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace KAIROSV2.Business.Managers
{
    public class ProductosManager : ManagerBase, IProductosManager
    {
        private readonly IProductosRepository _productosRepository;
        private readonly IClaseProductoRepository _claseProductoRepository;
        private readonly ITipoProductoRepository _tipoProductoRepository;
        private readonly IAtributosProductoRepository _atributosProductoRepository;
        private readonly IRecetasRepository _recetasRepository;
        private readonly ITerminalesProductosRecetasRepository _terminalesProductosRecetasRepository;
        private readonly IProductosEngine _productosEngine;
        private readonly ITerminalesRepository _terminalesRepository;

        public ProductosManager(IProductosRepository productosRepository,
       IClaseProductoRepository claseProductoRepository,
       ITipoProductoRepository tipoProductoRepository,
       IAtributosProductoRepository atributosProductoRepository,
       IRecetasRepository recetasRepository,
       ITerminalesProductosRecetasRepository terminalesProductosRecetasRepository,
       IProductosEngine productosEngine,
       ITerminalesRepository terminalesRepository,
        IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _productosRepository = productosRepository;
            _claseProductoRepository = claseProductoRepository;
            _tipoProductoRepository = tipoProductoRepository;
            _atributosProductoRepository = atributosProductoRepository;
            _recetasRepository = recetasRepository;
            _terminalesProductosRecetasRepository = terminalesProductosRecetasRepository;
            _productosEngine = productosEngine;
            _terminalesRepository = terminalesRepository;
        }

        public IEnumerable<TProducto> ObtenerProductos()
        {
            return _productosRepository.Get(new string[] { "IdClaseNavigation", "IdTipoNavigation" });
        }
        public IEnumerable<TProductosTipo> ObtenerTiposProducto()
        {
            return _tipoProductoRepository.Get();
        }
        public IEnumerable<TProductosClase> ObtenerClasesProducto()
        {
            return _claseProductoRepository.Get();
        }
        public TProducto ObtenerProductoConRecetas(string idProducto)
        {
            return _productosRepository.Get(idProducto, "IdClaseNavigation", "TProductosReceta", "TProductosReceta.TProductosRecetasComponentes.IdComponenteNavigation.IdTipoNavigation", "TProductosAtributos");
        }
        public TProducto ObtenerProducto(string idProducto)
        {
            return _productosRepository.Get(idProducto);
        }
        public IEnumerable<ProductoTerminalDto> ObtenerProductosTerminalesRecetas(string idTerminal)
        {
            var productos = _productosRepository.Get(new string[] { "IdClaseNavigation", "TProductosReceta", "TProductosReceta.TTerminalesProductosReceta" });
            var productosTerminal = productos.Select(p => new ProductoTerminalDto
            {
                Asignado = p.TProductosReceta?.Any(r => r.TTerminalesProductosReceta?.Any(t => t.IdTerminal == idTerminal && t.IdProducto == p.IdProducto) ?? false) ?? false,
                CodigoProducto = p.IdProducto,
                NombreCorto = p.NombreCorto,
                Icon = p.IdClaseNavigation.Icono,
                Recetas = p.TProductosReceta.Select(x => new RecetaProductoTerminalDto
                {
                    Asignada = x.TTerminalesProductosReceta?.Any(e => e.IdTerminal == idTerminal) ?? false,
                    NombreReceta = x.IdReceta
                }).ToList()

            }).ToList();

            return productosTerminal;
        }
        public ProductoTerminalDto ObtenerProductoRecetaAsignadoTerminal(string idTerminal, string idProducto)
        {
            var producto = _productosRepository.Get(idProducto, "IdClaseNavigation", "TProductosReceta.TProductosRecetasComponentes.IdComponenteNavigation.IdTipoNavigation", "TProductosReceta.TTerminalesProductosReceta");
            foreach (var recetas in producto.TProductosReceta)
                recetas.TTerminalesProductosReceta = recetas.TTerminalesProductosReceta.Where(e => e.IdTerminal == idTerminal)?.ToList();

            var productoAsignado = Mapper.Map<ProductoTerminalDto>(producto);
            productoAsignado.NombreTerminal = _terminalesRepository.Get(idTerminal)?.Terminal;
            return productoAsignado;
        }

        public ManagerResult AsignarProductoRecetaATerminal(ProductoTerminalDto productoTerminal)
        {
            var response = new ManagerResult() { Failures = new List<string>() };

            var vigencias = productoTerminal.Recetas.Where(e => e.Vigencias != null).SelectMany(r => r.Vigencias);
            var terminalesProductos = new List<TTerminalesProductosReceta>();
            terminalesProductos = Mapper.Map<List<TTerminalesProductosReceta>>(vigencias);
            var productoId = terminalesProductos.FirstOrDefault().IdProducto;
            var terminalId = terminalesProductos.FirstOrDefault().IdTerminal;

            response.Result = _productosEngine.ValidarVigencias(terminalesProductos, response.Failures);
            if (!response.Result)
                return response;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _terminalesProductosRecetasRepository.RemoveAllByProductTerminal(productoId,terminalId);

                    _terminalesProductosRecetasRepository.AddProductsRecipesTerminal(terminalesProductos);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new PersistEntityException("Ocurrio un error actualizando las platas proveedor.");
            }
            return response;
        }

        public bool BorrarProducto(string idProducto)
        {
            var result = false;

            if (_productosRepository.Exists(idProducto))
            {
                _productosRepository.Remove(idProducto);
                result = true;
                LogInformacion(LogAcciones.Eliminar, "Configuración", "Productos", "Productos", "T_Productos", "Producto " + idProducto + " eliminado");
            }

            return result;
        }
        public void BorrarProductoTerminalReceta(string idProducto, string idTerminal)
        {
            _terminalesProductosRecetasRepository.RemoveAllByProductTerminal(idProducto, idTerminal);
        }
        public ManagerResult CrearProducto(TProducto producto)
        {
            var response = new ManagerResult() { Failures = new List<string>() };

            if (_productosEngine.ValidateUniqueData(producto, response.Failures))
            {
                if (_productosEngine.ValidateRecipeComponentsPorportions(producto.TProductosReceta?.ToList(), response.Failures))
                {
                    foreach (var recipe in producto.TProductosReceta)
                    {
                        recipe.EditadoPor = "Admin";
                        recipe.UltimaEdicion = DateTime.Now;
                        recipe.TProductosRecetasComponentes.ToList().ForEach(c => { c.EditadoPor = recipe.EditadoPor; c.UltimaEdicion = recipe.UltimaEdicion; });
                        _productosEngine.ReorderProducts(recipe.TProductosRecetasComponentes.ToList());
                    }

                    _productosRepository.Add(producto);
                    response.Result = true;
                    LogInformacion(LogAcciones.Insertar, "Configuración", "Productos", "Productos", "T_Productos", "Producto " + producto?.IdProducto + " creado");
                }
            }

            return response;
        }
        public ManagerResult ActualizarProducto(TProducto producto)
        {
            var response = new ManagerResult() { Failures = new List<string>() };

            if (!_productosRepository.Exists(producto.IdProducto))
            {
                response.Failures.Add("El producto que trata de actualizar no existe");
                response.Result = false;
            }
            else
            {
                _productosRepository.Update(producto);
                response.Result = true;
                LogInformacion(LogAcciones.Actualizar, "Configuración", "Productos", "Productos", "T_Productos", "Producto " + producto?.IdProducto + " actualizado");
            }

            return response;
        }
        public bool AgregarActualizarAtributosProducto(IEnumerable<TProductosAtributo> atributos)
        {
            var result = true;
            foreach (var attr in atributos)
            {
                try
                {
                    if (_atributosProductoRepository.Exists(attr.IdProducto, attr.IdAtributo))
                    {
                        _atributosProductoRepository.Update(attr);
                        LogInformacion(LogAcciones.Actualizar, "Configuración", "Productos", "Productos", "T_Productos_Atributos", $"Producto {attr.IdProducto} - Atributo {attr.IdAtributo} actualizado");
                    }
                    else
                    {
                        _atributosProductoRepository.Add(attr);
                        LogInformacion(LogAcciones.Insertar, "Configuración", "Productos", "Productos", "T_Productos_Atributos", $"Producto {attr.IdProducto} - Atributo {attr.IdAtributo} creado");
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    LogError(LogAcciones.Actualizar, "Configuración", "Productos", "Vista gestión productos", "T_Productos_Atributos", $"Excepción atributo {attr.IdAtributo} - {attr.IdProducto}.", ex);
                }
            }
            return result;
        }
        public ManagerResult AgregarActualizarRecetasProductoFormulario(IEnumerable<RecetaDTO> recetas, string productId)
        {
            var response = new ManagerResult() { Result = true, Failures = new List<string>() };

            //Validación recetas
            if (_productosEngine.ValidateRecipeComponentsPorportions(recetas?.ToList(), response.Failures))
            {
                //Crea o actualiza las recetas que vienen en la lista
                if (recetas != null)
                {
                    foreach (var recetaDTO in recetas)
                    {
                        try
                        {
                            recetaDTO.IdProducto = productId;
                            _productosEngine.AgregarActualizarReceta(recetaDTO);
                            LogInformacion(LogAcciones.Insertar, "Configuración", "Productos", "Productos", "T_Productos_Recetas", $"Producto {productId} - Receta {recetaDTO?.IdReceta} creado/actualizado");
                        }
                        catch (Exception ex)
                        {
                            response.Result = false;
                            LogError(LogAcciones.Actualizar, "Configuración", "Productos", "Vista gestión productos", "T_Productos_Recetas", $"Producto {productId} - Receta {recetaDTO?.IdReceta} no creado/actualizado", ex);
                        }
                    }
                    //Borra todas la recetas que ya no se encuentran en la lista enviada
                    _recetasRepository.RemoveLeftover(recetas?.FirstOrDefault()?.IdProducto, recetas?.Select(e => e.IdReceta).ToArray());
                }
                else
                    _recetasRepository.RemoveRecipesByProduct(productId);
            }
            else
                response.Result = false;

            return response;
        }
        public IEnumerable<TProducto> ObtenerProductosPorClase(string idclase)
        {
            return _productosRepository.GetByClass(idclase, "IdTipoNavigation");
        }

    }
}