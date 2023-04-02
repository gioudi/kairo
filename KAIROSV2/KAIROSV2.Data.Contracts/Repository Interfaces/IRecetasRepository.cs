using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IRecetasRepository : IDataRepository<TProductosReceta>
    {
        bool Exists(string idReceta, string idProducto);
        void RemoveRecipesByProduct(string idProducto);
        void RemoveLeftover(string idProducto, params string[] idRecetas);
        void RemoveRecipeComponents(string idProducto, string idReceta);
        void AddRecipeComponents(IEnumerable<TProductosRecetasComponente> componentes);
    }
}
