using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KAIROSV2.Data
{
    public class TipoProductoRepository : DataRepositoryBase<TProductosTipo>, ITipoProductoRepository
    {
        protected override TProductosTipo GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TProductosTipo UpdateEntity(KAIROSV2DBContext entityContext, TProductosTipo entity)
        {
            throw new NotImplementedException();
        }
    }
}
