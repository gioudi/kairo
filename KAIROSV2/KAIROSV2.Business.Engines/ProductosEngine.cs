using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Engines;
using KAIROSV2.Business.Entities;
using System.Linq;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.Data.Contracts;
using AutoMapper;

namespace KAIROSV2.Business.Engines
{
    public class ProductosEngine : IProductosEngine
    {
        private readonly IProductosRepository _productosRepository;
        private readonly IRecetasRepository _recetasRepository;
        private readonly IMapper _mapper;

        public ProductosEngine(IProductosRepository productosRepository,
       IMapper mapper,
       IRecetasRepository recetasRepository)
        {
            _productosRepository = productosRepository;
            _recetasRepository = recetasRepository;
            _mapper = mapper;
        }
        public void ReorderProducts(List<TProductosRecetasComponente> components)
        {
            var order = 1;
            foreach (var component in components?.OrderByDescending(e => e.ProporcionComponente))
            {
                component.Posicion = order;
                order++;
            }
        }

        public bool ValidateUniqueData(TProducto producto, List<string> failures)
        {
            if (failures is null)
                failures = new List<string>();

            if (_productosRepository.Exists(producto.IdProducto))
                failures.Add("El producto ya existe en el sistema");
            if (_productosRepository.ExistsNombreCorto(producto.NombreCorto))
                failures.Add("Ya existe un producto con el mismo nombre corto");
            if (_productosRepository.ExistsNombreCorto(producto.NombreErp))
                failures.Add("Ya existe un producto con el mismo nombre ERP");

            return failures.Count == 0;
        }

        public bool ValidateRecipeComponentsPorportions(List<TProductosReceta> recipes, List<string> failures)
        {
            if (failures is null)
                failures = new List<string>(recipes?.Count ?? 0);

            if (recipes != null)
                foreach (var recipe in recipes)
                {
                    var total = recipe?.TProductosRecetasComponentes?.Where(c => !_productosRepository.IsAdditive(c.IdComponente)).Sum(e => e.ProporcionComponente);
                    if (total < 1000000 || total > 1000000)
                    {
                        failures.Add($"Los componentes de la receta ({recipe.IdReceta}) no totalizan un millón");
                    }
                }

            return failures.Count == 0;
        }

        public bool ValidateRecipeComponentsPorportions(List<RecetaDTO> recipes, List<string> failures)
        {
            if (failures is null)
                failures = new List<string>(recipes?.Count ?? 0);

            if (recipes != null)
                foreach (var recipe in recipes)
                {
                    var total = recipe?.Componentes?.Where(c => !_productosRepository.IsAdditive(c.IdComponente)).Sum(e => e.ProporcionComponente);
                    if (total < 1000000 || total > 1000000)
                    {
                        failures.Add($"Los componentes de la receta ({recipe.IdReceta}) no totalizan un millón");
                    }
                }

            return failures.Count == 0;
        }

        public void AgregarActualizarReceta(RecetaDTO recetaDTO)
        {
            var receta = _mapper.Map<TProductosReceta>(recetaDTO);
            ReorderProducts(receta.TProductosRecetasComponentes.ToList());
            receta.UltimaEdicion = DateTime.Now;
            receta.EditadoPor = "Admin";
            receta.TProductosRecetasComponentes.ToList().ForEach(e => { e.IdReceta = receta.IdReceta; e.IdProducto = receta.IdProducto; e.EditadoPor = "Admin"; e.UltimaEdicion = DateTime.Now; });
            //if (!string.IsNullOrEmpty(recetaDTO.IdRecetaCurrent) && recetaDTO.IdReceta != recetaDTO.IdRecetaCurrent)
            //{
            // //TODO call sp
            //}
            if (_recetasRepository.Exists(receta.IdReceta, receta.IdProducto))
            {
                //TODO: Update components
                _recetasRepository.RemoveRecipeComponents(receta.IdProducto, receta.IdReceta);
                _recetasRepository.AddRecipeComponents(receta.TProductosRecetasComponentes);
            }
            else
                _recetasRepository.Add(receta);
        }


        public bool ValidarVigencias(List<TTerminalesProductosReceta> recetasTerminal, List<string> failures)
        {
            var result = true;
            if (failures is null)
                failures = new List<string>();

            if(recetasTerminal != null)
            //Revisión que solo exista una vigencia sin fecha fin
            if (recetasTerminal.Count(e => e.FechaFin == null) > 1)
            {
                    failures.Add("Existe mas de una vigencia sin fecha fin");
                    return false;
            }
            if(!ValidarVigenciaRecetaFechaActual(recetasTerminal))
            {
                result = false;
                failures.Add("La fecha actual no se encuentra contemplada en ninguna de las vigencias");
            }
            if(!ValidarVigenciaRecetaFechasSobrepuestas(recetasTerminal))
            {
                result = false;
                failures.Add("Existen fechas sobrepuestas en las vigencias.");
            }
            if (!ValidarVigenciaRecetaFechasConsecutivas(recetasTerminal))
            {
                result = false;
                failures.Add("No todas las fechas de vigencias son consecutivas.");
            }

            return result;
        }

        //Revision que la fecha actual se encuentre en algun rango
        public bool ValidarVigenciaRecetaFechaActual(List<TTerminalesProductosReceta> recetasTerminal)
        {
            var fechaActual = DateTime.Now.AddMinutes(-1);
            var result = true;
            var activoFuturo = recetasTerminal.FirstOrDefault(t => t.FechaFin == null)?.FechaInicio >= DateTime.Now;
            var activoRango = false;
            foreach (var vigencia in recetasTerminal)
            {
                if (fechaActual >= vigencia.FechaInicio && ((fechaActual <= vigencia.FechaFin) || !vigencia.FechaFin.HasValue))
                {
                    activoRango = true;
                    break;
                }
            }
            if (!activoFuturo && !activoRango)
                result = false;

            return result;
        }

        //Revision que las fechas no se sobrepongan
        public bool ValidarVigenciaRecetaFechasSobrepuestas(List<TTerminalesProductosReceta> recetasTerminal)
        {
            var result = true;
            var vigencias = recetasTerminal.OrderBy(e => e.FechaInicio);
            foreach (var vigencia in vigencias)
            {
                if (vigencias.Count(e => vigencia.FechaInicio.AddMinutes(-1) >=  e.FechaInicio && vigencia.FechaInicio.AddMinutes(1) <= e.FechaFin) > 1)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        //Revisar que las fechas sean consecutivas
        public bool ValidarVigenciaRecetaFechasConsecutivas(List<TTerminalesProductosReceta> recetasTerminal)
        {
            var result = true;
            var fechasVigenciasConsecutivas = recetasTerminal.OrderBy(e => e.FechaInicio).ToList();
            var ultimoIndex = fechasVigenciasConsecutivas.Count - 1;
            for (int i = 0; i < fechasVigenciasConsecutivas.Count; i++)
            {
                if (i != ultimoIndex)
                {
                    if (fechasVigenciasConsecutivas[i].FechaFin.GetValueOrDefault().AddMinutes(1) != fechasVigenciasConsecutivas[i + 1].FechaInicio)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }
    }
}