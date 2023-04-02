using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;

namespace KAIROSV2.Data.Contracts
{
    public interface ILogRepository : IDataRepository<TLog>
    {
        IEnumerable<TLog> GetLogsByDates(DateTime StartDate, DateTime FinishDate);
        Task AddAsync(TLog usuario, CancellationToken cancellationToken);
        IEnumerable<TLogAcciones> ObtenerLogAcciones();
        IEnumerable<TLogPrioridades> ObtenerLogPrioridades();
    }
}
