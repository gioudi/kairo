using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IAtributosProductoRepository : IDataRepository<TProductosAtributo>
    {
        bool Exists(string idProducto, int idAtributo);
        TProductosAtributo Get(string idProducto, int idAtributo, params string[] include);
        IEnumerable<TProductosAtributo> Get(string idProducto);
    }
}
