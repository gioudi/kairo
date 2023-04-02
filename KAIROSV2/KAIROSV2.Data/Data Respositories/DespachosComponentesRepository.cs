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
using System.Threading;

namespace KAIROSV2.Data
{
    public class DespachosComponentesRepository : DataRepositoryBase<TDespachosComponente>, IDespachosComponentesRepository
    {

        protected override TDespachosComponente GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TDespachoComponentesSet
                         where e.Id_Despacho == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TDespachosComponente UpdateEntity(KAIROSV2DBContext entityContext, TDespachosComponente entity)
        {
            return (from e in entityContext.TDespachoComponentesSet
                    where e.No_Orden == entity.No_Orden && 
                    e.Ship_To == entity.Ship_To && 
                    e.Id_Producto_Componente == entity.Id_Producto_Componente &&
                    e.Compartimento == entity.Compartimento &&
                    e.Tanque == entity.Tanque &&
                    e.Contador == entity.Contador
      
                    select e).FirstOrDefault();
        }

        public TDespachosComponente Obtener(string No_Orden, int Ship_To, string Id_Producto_Componente, int Compartimento, string Tanque, string Contador)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoComponentesSet.First(e => e.No_Orden == No_Orden && e.Ship_To == Ship_To && e.Id_Producto_Componente == Id_Producto_Componente && e.Compartimento == Compartimento && e.Contador == Contador);
            }
        }
        public TDespachosComponente Obtener(string No_Orden, int Ship_To, string Id_Producto_Componente, int Compartimento, string Tanque, string Contador , params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoComponentesSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.No_Orden == No_Orden && e.Ship_To == Ship_To && e.Id_Producto_Componente == Id_Producto_Componente && e.Compartimento == Compartimento && e.Contador == Contador).FirstOrDefault();
            }
        }

        public async Task<TDespachosComponente> ObtenerAsync(string No_Orden, int Ship_To, string Id_Producto_Componente, int Compartimento, string Tanque, string Contador)       {

            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoComponentesSet.Where(e => e.No_Orden == No_Orden && e.Ship_To == Ship_To && e.Id_Producto_Componente == Id_Producto_Componente && e.Compartimento == Compartimento && e.Contador == Contador).FirstOrDefault();
            }
        }
                
        public IEnumerable<TDespachosComponente> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoComponentesSet.ToList();
            }
        }

        public IEnumerable<TDespachosComponente> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoComponentesSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public async Task<IEnumerable<TDespachosComponente>> ObtenerTodasAsync(params string[] includes)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoComponentesSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }


        public IEnumerable<TDespachosComponente> ObtenerDespachosComponentesAPI( string searchQuery)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TDespachoComponentesSet.AsQueryable();

                if (!string.IsNullOrEmpty(searchQuery))
                    query = query.Where(searchQuery);
                
                return query;
            }
        }


        public async Task<IEnumerable<TDespachosComponente>> ObtenerTodasAsync()
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoComponentesSet.ToList();
            }
        }

        public bool Existe( string No_Orden , int Ship_To , string Id_Producto_Componente , int Compartimento , string Tanque , string Contador)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoComponentesSet.Any(e => e.No_Orden == No_Orden && e.Ship_To == Ship_To && e.Id_Producto_Componente == Id_Producto_Componente && e.Compartimento == Compartimento && e.Contador == Contador );
            }
        }

        public async Task<bool> ExisteAsync(string No_Orden, int Ship_To, string Id_Producto_Componente, int Compartimento, string Tanque, string Contador)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TDespachoComponentesSet.Any(e => e.No_Orden == No_Orden && e.Ship_To == Ship_To && e.Id_Producto_Componente == Id_Producto_Componente && e.Compartimento == Compartimento && e.Contador == Contador);
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

        public Task ActualizarAsync(TDespachosComponente Componente, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.TDespachoComponentesSet.Attach(Componente);
                entityContext.Entry(Componente).Property(x => x.Id_Despacho).IsModified = true;
                return entityContext.SaveChangesAsync(cancellationToken);
            }
        }

        public void Actualizar(TDespachosComponente Componente)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.TDespachoComponentesSet.Attach(Componente);
                entityContext.Entry(Componente).Property(x => x.Id_Despacho).IsModified = true;
                entityContext.SaveChanges();
            }
        }



    }
}
