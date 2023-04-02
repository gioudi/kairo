using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;

namespace KAIROSV2.Data.Contracts
{
    public interface IPermisosRepository : IDataRepository<TUPermiso>
    {
        IEnumerable<TUPermiso> GetPermissionByRol(string idRol);
        IEnumerable<TUPermiso> Get(params string[] includes);
    }
}
