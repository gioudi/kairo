using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.Business.Entities.Enums;
using KAIROSV2.WebApp.Identity.Authorization;
using KAIROSV2.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Controllers
{
    [Authorize]
    [PermissionsAuthorize(Permissions.SubModuloTablasCorreccion)]
    public class TablasCorreccionController : BaseController
    {
        private const string Vista = "Tablas Corrección";

        private readonly ITablasCorreccionManager _tablasCorreccionManager;

        public TablasCorreccionController(ITablasCorreccionManager tablasCorreccionManager)
        {
            Area = "Tablas de sistema";
            _tablasCorreccionManager = tablasCorreccionManager;
        }

        public IActionResult Index()
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, "", $"Ingreso a vista {Vista}");
            return View(new ListViewModel<object>
            {
                Encabezados = new List<string>() { "Api Observado", "Temperatura", "Api Corregido" }
            });
        }
        
        [HttpPost]
        public IActionResult PartialTablaCorreccion([FromBody] string tipoTabla)
        {
            LogInformacion(LogAcciones.IngresoVista, Vista, $"T_API_Corrección_{tipoTabla}", $"Ingreso a vista {Vista}");

            var viewModel = new ListViewModel<object>
            {
                Encabezados = tipoTabla switch
                {
                    "5b" => new List<string>() { "Api Observado", "Temperatura", "Api Corregido" },
                    "6b" => new List<string>() { "Api Corregido", "Temperatura", "Factor Corrección" },
                    "6cAlcohol" => new List<string>() { "Api Corregido", "Temperatura", "Factor Corrección" },
                    _ => null,
                }
            };

            return PartialView("_TablasCorreccion", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerDatosCorreccion5b([FromForm] DatatableViewModel datatableViewModel)
        {
            var orderName = datatableViewModel.columns[datatableViewModel.order.FirstOrDefault().column].data;
            var orderBy = $"{orderName} {datatableViewModel.order.FirstOrDefault().dir}";
            var search = new List<SearchDataValue>(datatableViewModel.columns.Count(c => !string.IsNullOrEmpty(c.search.value)));
            datatableViewModel.columns.ForEach(e =>
            {
                if (!string.IsNullOrEmpty(e.search.value))
                    search.Add(new SearchDataValue() { Property = e.name, Value = e.search.value });
            });

            var result = _tablasCorreccionManager.ObtenerCorrecion5b(datatableViewModel.start, datatableViewModel.length, orderBy, search);
            result.draw = datatableViewModel.draw;
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerDatosCorreccion6b([FromForm] DatatableViewModel datatableViewModel)
        {
            var orderName = datatableViewModel.columns[datatableViewModel.order.FirstOrDefault().column].data;
            var orderBy = $"{orderName} {datatableViewModel.order.FirstOrDefault().dir}";
            var search = new List<SearchDataValue>(datatableViewModel.columns.Count(c => !string.IsNullOrEmpty(c.search.value)));
            datatableViewModel.columns.ForEach(e =>
            {
                if (!string.IsNullOrEmpty(e.search.value))
                    search.Add(new SearchDataValue() { Property = e.name, Value = e.search.value });
            });

            var result = _tablasCorreccionManager.ObtenerCorrecion6b(datatableViewModel.start, datatableViewModel.length, orderBy, search);
            result.draw = datatableViewModel.draw;
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerDatosCorreccion6cAlcohol([FromForm] DatatableViewModel datatableViewModel)
        {
            var orderName = datatableViewModel.columns[datatableViewModel.order.FirstOrDefault().column].data;
            var orderBy = $"{orderName} {datatableViewModel.order.FirstOrDefault().dir}";
            var search = new List<SearchDataValue>(datatableViewModel.columns.Count(c => !string.IsNullOrEmpty(c.search.value)));
            datatableViewModel.columns.ForEach(e =>
            {
                if (!string.IsNullOrEmpty(e.search.value))
                    search.Add(new SearchDataValue() { Property = e.name, Value = e.search.value });
            });

            var result = _tablasCorreccionManager.ObtenerCorrecion6cAlcohol(datatableViewModel.start, datatableViewModel.length, orderBy, search);
            result.draw = datatableViewModel.draw;
            return Json(result);
        }
    }
}
