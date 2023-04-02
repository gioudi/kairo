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
    public class TASCortesRepository : DataRepositoryBase<TTASCortes>, ITASCortesRepository
    {

        protected override TTASCortes GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TTASCortesSet
                         where e.Id_Corte == Convert.ToInt32(id)
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TTASCortes UpdateEntity(KAIROSV2DBContext entityContext, TTASCortes entity)
        {
            return (from e in entityContext.TTASCortesSet
                    where e.Id_Corte == entity.Id_Corte
                    select e).FirstOrDefault();
        }

        public TTASCortes Obtener(int id, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTASCortesSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.Id_Corte == id).FirstOrDefault();
            }
        }

        public async Task<TTASCortes> ObtenerAsync(int Id_Corte)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Where(e => e.Id_Corte == Id_Corte).FirstOrDefault();
            }
        }

        public IEnumerable<TTASCortes> Obtener(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTASCortesSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public TTASCortes ObtenerIdPorFechaCierre(DateTime FechaCierre, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTASCortesSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.Where(e => e.Fecha_Cierre_Kairos == FechaCierre).FirstOrDefault();
            }
        }

        public async Task<TTASCortes> ObtenerIdPorFechaCierreAsync(DateTime FechaCierre)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Where(e => e.Fecha_Cierre_Kairos == FechaCierre).FirstOrDefault();
            }
        }

        public long ObtenerIdPorFechaCierre(DateTime FechaCierre)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Where(e => e.Fecha_Cierre_Kairos == FechaCierre).FirstOrDefault().Id_Corte;
            }
        }

        public IEnumerable<long> ObtenerIdsPorFechaCorte(DateTime FechaCorte)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Where(e => e.Fecha_Corte == FechaCorte).Select(e => e.Id_Corte).ToList();
            }
        }

        public List<long> ObtenerIdsPorFechasCierre(List<DateTime?> FechaCierre)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Where(e => FechaCierre.Contains(e.Fecha_Cierre_Kairos)).Select(e => e.Id_Corte).ToList();
            }
        }

        public IEnumerable<TTASCortes> ObtenerTASCortesPorFechaCorte(DateTime FechaCorte)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Where(e => e.Fecha_Corte.Date == FechaCorte.Date).ToList();
            }
        }

        public IEnumerable<TTASCortes> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.ToList();
            }
        }

        public IEnumerable<TTASCortes> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTASCortesSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public async Task<IEnumerable<TTASCortes>> ObtenerTodasAsync(params string[] includes)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTASCortesSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }



        public bool Existe(int id)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Any(e => e.Id_Corte == id);
            }
        }

        public async Task<bool> ExisteAsync(int Id_Corte)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Any(e => e.Id_Corte == Id_Corte);
            }
        }

        public bool ExisteFechaCierre(DateTime FechaCierre)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Any(e => e.Fecha_Cierre_Kairos == FechaCierre);
            }
        }

        public async Task<bool> ExisteFechaCierreAsync(DateTime FechaCierre)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Any(e => e.Fecha_Cierre_Kairos == FechaCierre);
            }
        }

        public bool ExisteFechaCortePorTerminal(DateTime FechaCorte , string Id_Terminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Any(e => e.Fecha_Corte.Date == FechaCorte.Date && e.Id_Terminal == Id_Terminal);
            }
        }

        public bool ExisteFechaCorte(DateTime FechaCorte)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Any(e => e.Fecha_Corte.Date == FechaCorte.Date);
            }
        }

        public async Task<bool> ExisteFechaCorteAsync(DateTime FechaCorte)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Any(e => e.Fecha_Corte.Date == FechaCorte.Date);
            }
        }

        public void Eliminar(int id)
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

        public TTASCortes ObtenerPrimerCierre()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Where(e => e.Fecha_Cierre_Kairos != null).OrderBy(e => e.Fecha_Cierre_Kairos).FirstOrDefault();
            }
        }

        public IEnumerable<TTASCortes> ObtenerFechasCierrePorMes(int año, int mes, string idTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Where(e => e.Id_Terminal == idTerminal &&
                                                             (e.Fecha_Corte.Month == mes &&
                                                              e.Fecha_Corte.Year == año)).ToList();
            }
        }

        public DateTime ObtenerUltimaFechaCorte(string Id_Terminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTASCortesSet.Where(e => e.Id_Terminal == Id_Terminal).OrderByDescending(e => e.Fecha_Corte).FirstOrDefault().Fecha_Corte;
            }
        }
    }
}
