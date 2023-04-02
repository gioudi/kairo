using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Data.Contracts
{
    public interface IUsuariosRepository : IDataRepository<TUUsuario>
    {
        IEnumerable<TUUsuario> GetAll(params string[] includes);
        TUUsuario Get(string idUsuario, params string[] includes);
        bool Exists(string idUsuario);
    }
}
