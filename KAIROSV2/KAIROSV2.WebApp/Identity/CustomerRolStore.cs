using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using LightCore.Common.Contracts;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using KAIROSV2.WebApp.Identity.Authorization;

namespace KAIROSV2.WebApp.Identity
{
    public class CustomerRolStore : IRoleStore<TURole>, IRoleClaimStore<TURole>
    {
        private bool disposedValue;
        private readonly IdentityErrorDescriber _ErrorDescriber;
        private readonly IAccountRepository _AccountRepository;
        private readonly IRolesPermisosRepository _RolesPermisosReapository;

        public CustomerRolStore(IdentityErrorDescriber describer,
            IAccountRepository accountRepository,
            IRolesPermisosRepository rolesPermisosReapository)
        {
            if (describer == null)
                throw new ArgumentNullException(nameof(describer));

            if (accountRepository == null)
                throw new ArgumentNullException(nameof(accountRepository));

            if (rolesPermisosReapository == null)
                throw new ArgumentNullException(nameof(rolesPermisosReapository));

            _ErrorDescriber = describer;
            _AccountRepository = accountRepository;
            _RolesPermisosReapository = rolesPermisosReapository;
        }

        public async Task<IdentityResult> CreateAsync(TURole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            await _AccountRepository.AddRoleAsync(role, cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TURole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            try
            {
                await _AccountRepository.RemoveRoleAsync(role, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(_ErrorDescriber.ConcurrencyFailure());
            }
            return IdentityResult.Success;
        }

        public async Task<TURole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return await _AccountRepository.FindRoleByIdAsync(roleId, cancellationToken);
        }

        public async Task<TURole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return await _AccountRepository.FindRoleByNameAsync(normalizedRoleName, cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(TURole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.Nombre);
        }

        public Task<string> GetRoleIdAsync(TURole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.IdRol);
        }

        public Task<string> GetRoleNameAsync(TURole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.Nombre);
        }

        public Task SetNormalizedRoleNameAsync(TURole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            role.Nombre = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(TURole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            role.Nombre = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(TURole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            try
            {
                await _AccountRepository.UpdateRoleAsync(role, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(_ErrorDescriber.ConcurrencyFailure());
            }
            return IdentityResult.Success;
        }

        protected void ThrowIfDisposed()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~KAIROSUserStore()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<IList<Claim>> GetClaimsAsync(TURole role, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            var permissions = _RolesPermisosReapository.GetEnabled(role.IdRol);
            var claims = new List<Claim>() { new Claim("http://schemas.primax.co/identity/claims/permissions", permissions.CompressPermissionsIntoString()) };
            return await Task.FromResult(claims.ToList());
        }

        public Task AddClaimAsync(TURole role, Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(TURole role, Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}