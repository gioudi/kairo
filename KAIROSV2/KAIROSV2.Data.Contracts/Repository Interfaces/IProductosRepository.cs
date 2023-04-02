using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface IProductosRepository : IDataRepository<TProducto>
    {
        IEnumerable<TProductosTipo> ObtenerTiposProducto();
        IEnumerable<TProducto> ObtenerProductos();
        TProducto Get(string id, params string[] includes);
        IEnumerable<TProducto> Get(params string[] includes);
        IEnumerable<TProducto> GetByType(string type, params string[] includes);
        IEnumerable<TProducto> GetByClass(string classId, params string[] includes);
        bool Exists(string id);
        bool ExistsNombreERP(string nombreERP);
        bool ExistsNombreCorto(string nombreCorto);
        bool IsAdditive(string idProduct);
        void Remove(string id);
    }
}
