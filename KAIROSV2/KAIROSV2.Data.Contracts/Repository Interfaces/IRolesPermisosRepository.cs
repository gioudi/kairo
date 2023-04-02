using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;

namespace KAIROSV2.Data.Contracts
{
    public interface IRolesPermisosRepository : IDataRepository<TURolesPermiso>
    {
        IEnumerable<TURolesPermiso> Get(string idRol, params string[] includes);
        IEnumerable<TURolesPermiso> GetEnabled(string idRol, params string[] includes);
        void RemoveAllByRol(string idRol);
    }
}
