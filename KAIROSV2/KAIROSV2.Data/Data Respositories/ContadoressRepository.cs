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
    public class ContadoresRepository : DataRepositoryBase<TContador>, IContadoresRepository
    {
        protected override TContador GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TContadorSet
                         where e.IdContador == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TContador UpdateEntity(KAIROSV2DBContext entityContext, TContador entity)
        {
            return (from e in entityContext.TContadorSet
                    where e.IdContador == entity.IdContador
                    select e).FirstOrDefault();
        }

        public IEnumerable<TContador> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TContadorSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TContador> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TContadorSet.ToList();
            }
        }

        public async Task<IEnumerable<TContador>> ObtenerTodasAsync()
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TContadorSet.ToList();
            }
        }        

        public TContador Obtener(string IdContador, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TContadorSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdContador == IdContador );
            }
        }

        public TContador Obtener(string IdContador)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TContadorSet.Where(e => e.IdContador == IdContador ).FirstOrDefault();
            }
        }

        //public IEnumerable<TContadoresTipo> ObtenerTipos()
        //{
        //    using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
        //    {
        //        return entityContext.TContadoresTipoSet.ToList();
        //    }
        //}

        //public IEnumerable<TContadoresEstados> ObtenerEstados()
        //{
        //    using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
        //    {
        //        return entityContext.TContadoresEstadosSet.ToList();
        //    }
        //}


        public async Task<TContador> ObtenerAsync(string IdContador)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TContadorSet.Where(e => e.IdContador == IdContador ).FirstOrDefault();
            }
        }

        public bool Existe(string IdContador)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TContadorSet.Any(e => e.IdContador == IdContador );
            }
        }

        public async Task<bool> ExisteAsync(string IdContador)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TContadorSet.Any(e => e.IdContador == IdContador );
            }
        }        

    }
}
