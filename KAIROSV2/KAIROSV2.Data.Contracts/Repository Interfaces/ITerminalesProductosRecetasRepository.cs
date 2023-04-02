using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface ITerminalesProductosRecetasRepository : IDataRepository<TTerminalesProductosReceta>
    {
        void RemoveAllByProductTerminal(string idProducto, string idTerminal);
        public void AddProductsRecipesTerminal(IEnumerable<TTerminalesProductosReceta> entities);
        IEnumerable<TTerminalesProductosReceta> Get(params string[] includes);
        
    }
}
