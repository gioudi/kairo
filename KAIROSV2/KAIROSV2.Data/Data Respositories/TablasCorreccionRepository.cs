using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace KAIROSV2.Data
{
    public class TablasCorreccionRepository : DataRepositoryBase<TApiCorreccion5b>, ITablasCorreccionRepository
    {
        public IEnumerable<TApiCorreccion5b> GetAPICorrecion5b(int skip, int pageSize, string sortExpression, string searchQuery, out int totalRecords)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TApiCorreccion5bSet.AsQueryable();

                if (!string.IsNullOrEmpty(searchQuery))
                    query = query.Where(searchQuery);

                if (!string.IsNullOrEmpty(sortExpression))
                    query = query.OrderBy(sortExpression);

                totalRecords = query.Count();

                return query.Skip(skip).Take(pageSize).ToList();
            }
        }

        public IEnumerable<TApiCorreccion6b> GetAPICorrecion6b(int skip, int pageSize, string sortExpression, string searchQuery, out int totalRecords)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TApiCorreccion6bSet.AsQueryable();

                if (!string.IsNullOrEmpty(searchQuery))
                    query = query.Where(searchQuery);

                if (!string.IsNullOrEmpty(sortExpression))
                    query = query.OrderBy(sortExpression);

                totalRecords = query.Count();

                return query.Skip(skip).Take(pageSize).ToList();
            }
        }

        public IEnumerable<TApiCorreccion6cAlcohol> GetAPICorrecion6cAlcohol(int skip, int pageSize, string sortExpression, string searchQuery, out int totalRecords)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TApiCorreccion6cAlcoholSet.AsQueryable();

                if (!string.IsNullOrEmpty(searchQuery))
                    query = query.Where(searchQuery);

                if (!string.IsNullOrEmpty(sortExpression))
                    query = query.OrderBy(sortExpression);

                totalRecords = query.Count();

                return query.Skip(skip).Take(pageSize).ToList();
            }
        }

        public double GetCorrecion5b(double ApiObservado , double Temperatura)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TApiCorreccion5bSet.First(e => e.ApiObservado == ApiObservado && e.Temperatura == Temperatura).ApiCorregido;
            }
        }

        public double GetCorrecion6b(double ApiCorregido, double Temperatura)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TApiCorreccion6bSet.First(e => e.ApiCorregido == ApiCorregido && e.Temperatura == Temperatura).FactorCorreccion;
            }
        }

        public bool ExisteCorrecion6CAlcohol(double ApiCorregido, double Temperatura)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TApiCorreccion6cAlcoholSet.Any(e => e.ApiCorregido == ApiCorregido && e.Temperatura == Temperatura);
            }
        }

        public bool ExisteCorrecion5b(double ApiObservado, double Temperatura)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TApiCorreccion5bSet.Any(e => e.ApiObservado == ApiObservado && e.Temperatura == Temperatura);
            }
        }

        public bool ExisteCorrecion6b(double ApiCorregido, double Temperatura)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TApiCorreccion6bSet.Any(e => e.ApiCorregido == ApiCorregido && e.Temperatura == Temperatura);
            }
        }

        public double GetCorrecion6CAlcohol(double ApiCorregido, double Temperatura)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TApiCorreccion6cAlcoholSet.First(e => e.ApiCorregido == ApiCorregido && e.Temperatura == Temperatura).FactorCorreccion;
            }
        }

        protected override TApiCorreccion5b GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TApiCorreccion5b UpdateEntity(KAIROSV2DBContext entityContext, TApiCorreccion5b entity)
        {
            return (from e in entityContext.TApiCorreccion5bSet
                    where e.ApiObservado == entity.ApiObservado && e.Temperatura == entity.Temperatura
                    select e).FirstOrDefault();
        }
    }
}
