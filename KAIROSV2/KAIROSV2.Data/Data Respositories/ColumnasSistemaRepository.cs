using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data
{
    public class ColumnasSistemaRepository : DataRepositoryBase<VDbColumna>, IColumnasSistemaRepository
    {
        protected override VDbColumna GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.VDbColumnas
                         where e.ObjectId == int.Parse(id.ToString())
                         select e);

            var results = query.FirstOrDefault();
            return results;
        }

        public IEnumerable<VDbColumna> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.VDbColumnas.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<VDbColumna> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.VDbColumnas.ToList();
            }
        }



        public IEnumerable<VDbColumna> ObtenerColumnasTabla(int idTabla)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.VDbColumnas.Where(e => e.ObjectId == idTabla).ToList();
            }
        }

        public async Task<IEnumerable<VDbColumna>> ObtenerAsync(int idTabla)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.VDbColumnas.Where(e => e.ObjectId == idTabla);
            }
        }



        protected override VDbColumna UpdateEntity(KAIROSV2DBContext entityContext, VDbColumna entity)
        {
            throw new NotImplementedException();
        }
    }
}

