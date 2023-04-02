using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using KAIROSV2.Business.Common.Exceptions;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace KAIROSV2.Data
{
    public class DespachosRepository : DataRepositoryBase<TDespacho>, IDespachosRepository
    {

        protected override TDespacho GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TDespachoSet
                         where e.Id_Despacho == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TDespacho UpdateEntity(KAIROSV2DBContext entityContext, TDespacho entity)
        {
            return (from e in entityContext.TDespachoSet
                    where e.Id_Despacho == entity.Id_Despacho
                    select e).FirstOrDefault();
        }

        public TDespacho Obtener(string id)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoSet.First(e => e.Id_Despacho == id);
            }
        }
        public TDespacho Obtener(string id, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.Id_Despacho == id).FirstOrDefault();
            }
        }

        public async Task<TDespacho> ObtenerAsync(string Id_Despacho)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoSet.Where(e => e.Id_Despacho == Id_Despacho ).FirstOrDefault();
            }
        }

        public IEnumerable<TDespacho> Obtener(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TDespacho> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoSet.ToList();
            }
        }

        public IEnumerable<TDespacho> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public async Task<IEnumerable<TDespacho>> ObtenerTodasAsync(params string[] includes)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }


        public IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte( string Id_Terminal , string Id_Compañia , List<long> Id_Corte )
        {
            List<long?> Cortes = new List<long?>();

            foreach (var corte in Id_Corte) { Cortes.Add(corte); }

            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var TerminalesCompaniasCorte =  entityContext.TDespachoSet.Where( e => e.Id_Terminal == Id_Terminal && e.Id_Compañia == Id_Compañia).ToList();
                return TerminalesCompaniasCorte.Where(e => Cortes.Contains(e.Id_Corte)).ToList(); 
            }
        }

       
        public IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, List<string> Id_Compañia, List<long> Id_Corte)
        {
            List<long?> Cortes = new List<long?>();

            foreach(var corte in Id_Corte) { Cortes.Add(corte); }

            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var TerminalesCorte = entityContext.TDespachoSet.Where(e => e.Id_Terminal == Id_Terminal).ToList();
                var CompaniasTerminalCortes = TerminalesCorte.Where(e => Id_Compañia.Contains(e.Id_Compañia)).ToList();
                return CompaniasTerminalCortes.Where(e => Cortes.Contains(e.Id_Corte)).ToList();
            }
        }

        public IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, List<string> Id_Compañia, long Id_Corte)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var TerminalesCorte =  entityContext.TDespachoSet.Where(e => e.Id_Terminal == Id_Terminal && e.Id_Corte == Id_Corte).ToList();
                return TerminalesCorte.Where(e => Id_Compañia.Contains(e.Id_Compañia)).ToList();
            }
        }

        public IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, string Id_Compañia, long Id_Corte, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.Id_Terminal == Id_Terminal && e.Id_Compañia == Id_Compañia && e.Id_Corte == Id_Corte ).ToList();

            }
        }

        public IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, List<string> Id_Compañia, long Id_Corte, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.Id_Terminal == Id_Terminal && e.Id_Corte == Id_Corte).Where(e => Id_Compañia.Contains(e.Id_Compañia)).ToList();

            }
        }

        public IEnumerable<TDespacho> ObtenerDespachosTerminalCorte(string Id_Terminal,  long Id_Corte, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.Id_Terminal == Id_Terminal && e.Id_Corte == Id_Corte).ToList();

            }
        }

        public IEnumerable<TDespachosComponente> ObtenerDespachosComponentesPorIds(IEnumerable<string> Despachos)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoComponentesSet.Where(x => Despachos.Any(y => y == x.Id_Despacho)).ToList();

            }
        }

        public IEnumerable<string> ObtenerIdsDespachosTerminalCompañiaProductoCorte(string Id_Terminal, string Id_Compañia,string Id_Producto, List<long> Id_Corte)
        {
            List<long?> Cortes = new List<long?>();

            foreach (var corte in Id_Corte) { Cortes.Add(corte); }

            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoSet.Where(e => e.Id_Terminal == Id_Terminal && e.Id_Compañia == Id_Compañia && e.Id_Producto_Despacho == Id_Producto).Where(e => Cortes.Contains(e.Id_Corte)).Select(e => e.Id_Despacho).ToList();

            }
        }

        public IEnumerable<string> ObtenerIdsDespachosTerminalProducto(string Id_Terminal, string Id_Producto )
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoSet.Where(e => e.Id_Terminal == Id_Terminal && e.Id_Producto_Despacho == Id_Producto && (e.Id_Corte == 0 || e.Id_Corte == null)).Select(e => e.Id_Despacho).ToList();

            }
        }

        public IEnumerable<string> ObtenerIdsDespachosTerminalCompaniaProducto(string Id_Terminal, string Id_Compania , string Id_Producto)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoSet.Where(e => e.Id_Terminal == Id_Terminal && e.Id_Compañia == Id_Compania && e.Id_Producto_Despacho == Id_Producto && (e.Id_Corte == 0 || e.Id_Corte == null)).Select(e => e.Id_Despacho).ToList();

            }
        }



        public IEnumerable<string> ObtenerIdsDespachosTerminalCorte(string Id_Terminal, List<long> Id_Corte)
        {
            List<long?> Cortes = new List<long?>();

            foreach (var corte in Id_Corte) { Cortes.Add(corte); }

            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoSet.Where(e => e.Id_Terminal == Id_Terminal).Where(e => Cortes.Contains(e.Id_Corte)).Select(e => e.Id_Despacho).ToList();

            }
        }

        public async Task<IEnumerable<TDespacho>> ObtenerDespachosFiltrosAsync(string Id_Terminal, string Id_Compañia, long Id_Corte, params string[] includes)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TDespacho> ObtenerDespachosAPI(int skip, int pageSize, string sortExpression, string searchQuery, out int totalRecords)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();

                if (!string.IsNullOrEmpty(searchQuery))
                    query = query.Where(searchQuery);

                if (!string.IsNullOrEmpty(sortExpression))
                    query = query.OrderBy(sortExpression);

                totalRecords = query.Count();

                return query.Skip(skip).Take(pageSize).ToList();
            }
        }


        public IEnumerable<TDespacho> ObtenerDespachosAPI( string searchQuery)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();

                if (!string.IsNullOrEmpty(searchQuery))
                    query = query.Where(searchQuery);
                
                return query;
            }
        }


        public async Task<IEnumerable<TDespacho>> ObtenerTodasAsync()
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoSet.ToList();
            }
        }

        public bool Existe(string id)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoSet.Any(e => e.Id_Despacho == id);
            }
        }

        public async Task<bool> ExisteAsync(string Id_Despacho)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoSet.Any(e => e.Id_Despacho == Id_Despacho);
            }
        }

        public void Eliminar(string id)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var entity = GetEntity(entityContext, id);
                entityContext.Entry(entity).State = EntityState.Deleted;
                try
                {
                    entityContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;

                    if (sqlException?.Number == 547)
                        throw new DeleteCascadeException("La entidad que desea borrar tiene datos relacionados");
                    else
                        throw;
                }
            }
        }

        public IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, string Id_Compañia, List<long> Id_Corte, params string[] includes)
        {
            List<long?> Cortes = new List<long?>();

            foreach (var corte in Id_Corte) { Cortes.Add(corte); }

            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                var Despachos = query.Where(e => e.Id_Terminal == Id_Terminal && e.Id_Compañia == Id_Compañia).ToList();
                return Despachos.Where(e => Cortes.Contains(e.Id_Corte)).ToList();
            }
        }

        public IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaSinCorte(string Id_Terminal, string Id_Compañia, DateTime UltimaFechaCorte, DateTime FechaSeleccionada ,params string[] includes)
        {            
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.Id_Terminal == Id_Terminal && e.Id_Compañia == Id_Compañia && (e.Id_Corte == 0 || e.Id_Corte == null ) && e.Fecha_Final_Despacho <= FechaSeleccionada && e.Fecha_Final_Despacho >= UltimaFechaCorte ).ToList();
                
            }
        }

        public IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaSinCorte(string Id_Terminal, List<string> Id_Compañia, DateTime UltimaFechaCorte , DateTime FechaSeleccionada, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                var DespachosSinCortes = query.Where(e => e.Id_Terminal == Id_Terminal && (e.Id_Corte == 0 || e.Id_Corte == null) && e.Fecha_Final_Despacho >= UltimaFechaCorte && e.Fecha_Final_Despacho <= FechaSeleccionada).ToList();
                return DespachosSinCortes.Where(e => Id_Compañia.Contains(e.Id_Compañia)).ToList();
            }
        }

        public IEnumerable<TDespacho> ObtenerDespachosTerminalCompañiaCorte(string Id_Terminal, List<string> Id_Compañia, List<long> Id_Corte, params string[] includes)
        {
            List<long?> Cortes = new List<long?>();

            foreach (var corte in Id_Corte) { Cortes.Add(corte); }

            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };
                var CompaniasTerminalCortes = query.Where(e => Id_Compañia.Contains(e.Id_Compañia)).ToList();
                return CompaniasTerminalCortes.Where(e => Cortes.Contains(e.Id_Corte)).ToList();
            }
        }
    }
}
