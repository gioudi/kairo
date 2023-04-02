using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface ITanquesRepository : IDataRepository<TTanque>
    {
        IEnumerable<TTanque> ObtenerTodas(params string[] includes);
        
        IEnumerable<TTanque> ObtenerTodas();
        Task<IEnumerable<TTanque>> ObtenerTodasAsync();

        IEnumerable<TTanquesEstado> ObtenerEstadosTanque();

        TTanque Obtener(string IdTanque, string IdTerminal, params string[] includes);
        
        TTanque Obtener(string IdTanque, string IdTerminal);

        Task<TTanque> ObtenerAsync(string IdTanque, string IdTerminal);

        bool Existe(string IdTanque, string IdTerminal);
        Task <bool> ExisteAsync(string IdTanque, string IdTerminal);
        void BorrarPantallaFlotante(string IdTanque, string IdTerminal);
    }
}
