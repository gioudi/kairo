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
    public class TablasSistemaRepository : DataRepositoryBase<VDbTabla>, ITablasSistemaRepository
    {
        protected override VDbTabla GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.VDbTablas
                         where e.ObjectId == int.Parse(id.ToString())
                         select e);

            var results = query.FirstOrDefault();
            return results;
        }

        public IEnumerable<VDbTabla> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.VDbTablas.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<string> ObtenerNombresTablas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                List<string> nombreTablas = new List<string>();
                List<VDbTabla> tablas = entityContext.VDbTablas.ToList();
                foreach (VDbTabla tabla in tablas)
                {
                    nombreTablas.Add(tabla.Name);
                };

                return nombreTablas;
            }
        }

        public IEnumerable<VDbTabla> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.VDbTablas.ToList();
            }
        }


        public VDbTabla Obtener(string NameTabla, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.VDbTablas.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.Name == NameTabla);
            }
        }

        public VDbTabla Obtener(string NombreTabla)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.VDbTablas.Where(e => e.Name == NombreTabla).FirstOrDefault();
            }
        }

        public async Task<VDbTabla> ObtenerAsync(string NombreTabla)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.VDbTablas.Where(e => e.Name == NombreTabla).FirstOrDefault();
            }
        }

        public bool Existe(string NombreTabla)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.VDbTablas.Any(e => e.Name == NombreTabla);
            }
        }

        protected override VDbTabla UpdateEntity(KAIROSV2DBContext entityContext, VDbTabla entity)
        {
            throw new NotImplementedException();
        }
    }
}

