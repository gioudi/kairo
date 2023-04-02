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
    public class ProcesamientoArchivosDetRepository : DataRepositoryBase<TProcesamientoArchivosDet>, IProcesamientoArchivosDetRepository
    {
        protected override TProcesamientoArchivosDet GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TProcesamientoArchivosDetSet
                         where e.IdMapeo == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TProcesamientoArchivosDet UpdateEntity(KAIROSV2DBContext entityContext, TProcesamientoArchivosDet entity)
        {
            return (from e in entityContext.TProcesamientoArchivosDetSet
                    where e.IdMapeo == entity.IdMapeo && e.IdTabla == entity.IdTabla && e.IdCampo == entity.IdCampo
                    select e).FirstOrDefault();
        }

        public IEnumerable<TProcesamientoArchivosDet> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TProcesamientoArchivosDetSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TProcesamientoArchivosDet> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosDetSet.ToList();
            }
        }


        public TProcesamientoArchivosDet Obtener(string idMapeo, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TProcesamientoArchivosDetSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdMapeo == idMapeo);
            }
        }

        public List<TProcesamientoArchivosDet> ObtenerByKey(string idMapeo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosDetSet.Where(e => e.IdMapeo == idMapeo).ToList();
            }
        }


        public IEnumerable<string> ObtenerTablasSistemaByKey(string idMapeo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                List<string> res = (from p in entityContext.TProcesamientoArchivosDetSet
                .Where(e => e.IdMapeo == idMapeo)
                                    select p.IdTabla).ToList();
                return res.Distinct();
            }
        }


        public IEnumerable<TProcesamientoArchivosDet> ObtenerTablasSistemaByKeyTabla(string idMapeo, string idTabla)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosDetSet.Where
                       (e => (e.IdMapeo == idMapeo && e.IdTabla == idTabla)).ToList();
            }
        }

        public int IndexArchivoByKeyTabla(string idMapeo, string idTabla, string idCampo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return (from p in entityContext.TProcesamientoArchivosDetSet
                       .Where(e => (e.IdMapeo == idMapeo && e.IdTabla == idTabla && e.IdCampo == idCampo))
                        select p.IndiceColumna).FirstOrDefault();
            }
        }

        public string NombreColumnaArchivoByKeyTabla(string idMapeo, string idTabla, string idCampo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return (from p in entityContext.TProcesamientoArchivosDetSet
                       .Where(e => (e.IdMapeo == idMapeo && e.IdTabla == idTabla && e.IdCampo == idCampo))
                        select p.IdColumna).FirstOrDefault();
            }
        }


        public List<string> ObtenerColumnasArchivoByKey(string idMapeo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return (from p in entityContext.TProcesamientoArchivosDetSet
                      .Where(e => e.IdMapeo == idMapeo)
                        select p.IdColumna).Distinct().ToList();
            }
        }

        public string InsertData(string sql)
        {

            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                try
                {
                    int rowInserted = entityContext.Database.ExecuteSqlRaw(sql);
                    if (rowInserted == 1)
                    {
                        return "Exito";
                    }
                    else
                    {
                        return "Fallo";
                    }
                }
                catch (Exception e)
                {
                    return (e.Message);
                }
            }
        }



        public async Task<TProcesamientoArchivosDet> ObtenerAsync(string idMapeo)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosDetSet.Where(e => e.IdMapeo == idMapeo).FirstOrDefault();
            }
        }

        public bool Existe(string idMapeo, string idTabla, string idCampo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosDetSet.Any(e => (e.IdMapeo == idMapeo && e.IdCampo == idCampo && e.IdTabla == idTabla));
            }
        }


        public bool Existe(string idMapeo, string idTabla)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosDetSet.Any(e => (e.IdMapeo == idMapeo && e.IdTabla == idTabla));
            }
        }

        public bool Existe(string idMapeo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosDetSet.Any(e => (e.IdMapeo == idMapeo));
            }
        }

        public TProcesamientoArchivosDet Obtener(string idMapeo)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProcesamientoArchivosDetSet.Where(e => e.IdMapeo == idMapeo).FirstOrDefault();
            }
        }
    }
}

