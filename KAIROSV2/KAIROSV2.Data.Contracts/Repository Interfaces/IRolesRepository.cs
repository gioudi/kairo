using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;

namespace KAIROSV2.Data.Contracts
{
    public interface IRolesRepository : IDataRepository<TURole>
    {
        bool RolHasAssociatedUsers(string id);
        bool Exists(string idRol);
    }

}
