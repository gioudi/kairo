using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Data_Respositories
{
    public class LineasRepository : DataRepositoryBase<TLinea>, ILineasRepository
    {
        protected override TLinea GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TLineaSet
                         where e.IdLinea == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TLinea UpdateEntity(KAIROSV2DBContext entityContext, TLinea entity)
        {
            return (from e in entityContext.TLineaSet
                    where e.IdLinea == entity.IdLinea && e.IdTerminal == entity.IdTerminal
                    select e).FirstOrDefault();
        }
        public IEnumerable<TLinea> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TLineaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TLinea> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TLineaSet.ToList();
            }
        }

        public async Task<IEnumerable<TLinea>> ObtenerTodasAsync()
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TLineaSet.ToList();
            }
        }

        public IEnumerable<TLineasEstado> ObtenerEstadosLinea()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TLineaEstadoSet.ToList();
            }
        }

        public TLinea Obtener(string IdLinea, string IdTerminal, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TLineaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdLinea == IdLinea && e.IdTerminal == IdTerminal);
            }
        }

        public TLinea Obtener(string IdLinea, string IdTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TLineaSet.Where(e => e.IdLinea == IdLinea && e.IdTerminal == IdTerminal).FirstOrDefault();
            }
        }

        public async Task<TLinea> ObtenerAsync(string IdLinea, string IdTerminal)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TLineaSet.Where(e => e.IdLinea == IdLinea && e.IdTerminal == IdTerminal).FirstOrDefault();
            }
        }

        public bool Existe(string IdLinea, string IdTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TLineaSet.Any(e => e.IdLinea == IdLinea && e.IdTerminal == IdTerminal);
            }
        }

        public async Task<bool> ExisteAsync(string IdLinea, string IdTerminal)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TLineaSet.Any(e => e.IdLinea == IdLinea && e.IdTerminal == IdTerminal);
            }
        }
    }
}