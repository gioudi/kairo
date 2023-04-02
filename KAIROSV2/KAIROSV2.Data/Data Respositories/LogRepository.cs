using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KAIROSV2.Data
{
    public class LogRepository : DataRepositoryBase<TLog>, ILogRepository
    {
        public IEnumerable<TLog> GetLogsByDates(DateTime StartDate, DateTime FinishDate)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TLogSet.Where(e => e.FechaEvento >= StartDate && e.FechaEvento <= FinishDate).ToList();
            }
        }

        protected override TLog GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TLogSet
                         where e.Id == Convert.ToInt64(id)
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TLog UpdateEntity(KAIROSV2DBContext entityContext, TLog entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(TLog Registro, CancellationToken cancellationToken)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                await entityContext.TLogSet.AddAsync(Registro, cancellationToken);
                await entityContext.SaveChangesAsync(cancellationToken);
            }
        }

        public IEnumerable<TLogAcciones> ObtenerLogAcciones()
        {
            using ( KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TLogAccionesSet.ToList();
            }
        }

        public IEnumerable<TLogPrioridades> ObtenerLogPrioridades()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TLogPrioridadesSet.ToList();
            }
        }
    }
}
