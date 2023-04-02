using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data
{
    public class ClaseProductoRepository : DataRepositoryBase<TProductosClase>, IClaseProductoRepository
    {
        protected override TProductosClase GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TProductosClase UpdateEntity(KAIROSV2DBContext entityContext, TProductosClase entity)
        {
            throw new NotImplementedException();
        }
    }
}
