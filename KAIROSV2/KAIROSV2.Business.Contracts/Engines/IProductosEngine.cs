using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;

namespace KAIROSV2.Business.Contracts.Engines
{
    public interface IProductosEngine
    {
        void ReorderProducts(List<TProductosRecetasComponente> components);
        bool ValidateUniqueData(TProducto producto, List<string> failures);
        bool ValidateRecipeComponentsPorportions(List<TProductosReceta> recipes, List<string> failures);
        bool ValidateRecipeComponentsPorportions(List<RecetaDTO> recipes, List<string> failures);
        void AgregarActualizarReceta(RecetaDTO recetaDTO);
        bool ValidarVigencias(List<TTerminalesProductosReceta> recetasTerminal, List<string> failures);
        bool ValidarVigenciaRecetaFechaActual(List<TTerminalesProductosReceta> recetasTerminal);
        bool ValidarVigenciaRecetaFechasSobrepuestas(List<TTerminalesProductosReceta> recetasTerminal);
        bool ValidarVigenciaRecetaFechasConsecutivas(List<TTerminalesProductosReceta> recetasTerminal);
    }
}
