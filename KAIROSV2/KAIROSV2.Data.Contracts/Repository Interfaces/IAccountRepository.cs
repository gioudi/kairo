using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;

namespace KAIROSV2.Data.Contracts
{
    public interface IAccountRepository : IDataRepository<TUUsuario>
    {
        Task AddAsync(TUUsuario usuario, CancellationToken cancellationToken);
        Task RemoveAsync(TUUsuario usuario, CancellationToken cancellationToken);
        Task UpdateAsync(TUUsuario usuario, CancellationToken cancellationToken);
        Task UpdateUserRoleAsync(TUUsuario usuario, CancellationToken cancellationToken);
        Task<TUUsuario> FindByIdAsync(string id, CancellationToken cancellationToken);
        Task<TUUsuario> FindByNameAsync(string name, CancellationToken cancellationToken);
        Task<IList<TUUsuario>> GetByRolAsync(string rolId, CancellationToken cancellationToken);
        IQueryable<TUUsuario> GetIqueryable();

        Task AddRoleAsync(TURole role, CancellationToken cancellationToken);
        Task RemoveRoleAsync(TURole role, CancellationToken cancellationToken);
        Task<TURole> FindRoleByIdAsync(string id, CancellationToken cancellationToken);
        Task<TURole> FindRoleByNameAsync(string name, CancellationToken cancellationToken);
        Task UpdateRoleAsync(TURole role, CancellationToken cancellationToken);
    }
}
