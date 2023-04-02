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
    public class TerminalesRepository : DataRepositoryBase<TTerminal>, ITerminalesRepository
    {
        protected override TTerminal GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TTerminalSet
                         where e.IdTerminal == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TTerminal UpdateEntity(KAIROSV2DBContext entityContext, TTerminal entity)
        {
            return (from e in entityContext.TTerminalSet
                    where e.IdTerminal == entity.IdTerminal
                    select e).FirstOrDefault();
        }

        public IEnumerable<TTerminal> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTerminalSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TTerminal> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalSet.ToList();
            }
        }

        public async Task<IEnumerable<TTerminal>> ObtenerTodasAsync()
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalSet.ToList();
            }
        }

        public IEnumerable<TTerminalesEstado> ObtenerEstadosTerminal()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalesEstadoSet.ToList();
            }
        }

        public TTerminal Obtener(string IdTerminal, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTerminalSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdTerminal == IdTerminal);
            }
        }

        public TTerminal Obtener(string IdTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalSet.Where(e => e.IdTerminal == IdTerminal).FirstOrDefault();
            }
        }

        public async Task<TTerminal> ObtenerAsync(string IdTerminal)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalSet.Where(e => e.IdTerminal == IdTerminal).FirstOrDefault();
            }
        }        

        public bool Existe(string IdTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalSet.Any(e => e.IdTerminal == IdTerminal);
            }
        }

        public async Task<bool> ExisteAsync(string IdTerminal)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTerminalSet.Any(e => e.IdTerminal == IdTerminal);
            }
        }
    }
}
