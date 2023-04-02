using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface ITASCortesRepository : IDataRepository<TTASCortes>
    {
        TTASCortes Obtener(int id, params string[] includes);
        Task<TTASCortes> ObtenerAsync(int Id_Corte);
        IEnumerable<TTASCortes> ObtenerTodas();
        IEnumerable<TTASCortes> ObtenerTodas(params string[] includes);
        Task<IEnumerable<TTASCortes>> ObtenerTodasAsync(params string[] includes);
        IEnumerable<TTASCortes> Obtener(params string[] includes);
        long ObtenerIdPorFechaCierre(DateTime FechaCierre);
        IEnumerable<long> ObtenerIdsPorFechaCorte(DateTime FechaCorte);
        IEnumerable<TTASCortes> ObtenerTASCortesPorFechaCorte(DateTime FechaCorte);
        TTASCortes ObtenerIdPorFechaCierre(DateTime FechaCierre, params string[] includes);
        Task<TTASCortes> ObtenerIdPorFechaCierreAsync(DateTime FechaCierre);
        List<long> ObtenerIdsPorFechasCierre(List<DateTime?> FechaCierre);
        bool Existe(int id);
        Task<bool> ExisteAsync(int Id_Corte);
        bool ExisteFechaCierre(DateTime FechaCierre);
        Task<bool> ExisteFechaCierreAsync(DateTime FechaCierre);
        bool ExisteFechaCorte(DateTime FechaCorte);
        bool ExisteFechaCortePorTerminal(DateTime FechaCorte, string Id_Terminal);
        Task<bool> ExisteFechaCorteAsync(DateTime FechaCorte);
        void Eliminar(int id);
        TTASCortes ObtenerPrimerCierre();
        public IEnumerable<TTASCortes> ObtenerFechasCierrePorMes(int año, int mes, string idTerminal);
        public DateTime ObtenerUltimaFechaCorte(string Id_Terminal);
    }
}
