using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;

namespace KAIROSV2.Data.Contracts
{
    public interface IUsuariosTerminalCompañiaRepository : IDataRepository<TUUsuariosTerminalCompañia>
    {
        IEnumerable<TUUsuariosTerminalCompañia> Get(string idUsuario, params string[] includes);
        void RemoveAllByUser(string idUsuario);
        void RemoveAll();
    }
}
