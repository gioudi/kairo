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
using System.Globalization;

namespace KAIROSV2.WebApp.Identity
{
    public class CustomUserStore : IUserStore<TUUsuario>, IQueryableUserStore<TUUsuario>, IUserRoleStore<TUUsuario>
    {
        private bool disposedValue;
        private readonly IdentityErrorDescriber _ErrorDescriber;
        private readonly IAccountRepository _AccountRepository;

        public IQueryable<TUUsuario> Users
        {
            get { return _AccountRepository.GetIqueryable(); }
        }

        public CustomUserStore(IdentityErrorDescriber describer, IAccountRepository accountRepository)
        {
            if (describer == null)
                throw new ArgumentNullException(nameof(describer));

            if (accountRepository == null)
                throw new ArgumentNullException(nameof(accountRepository));

            _ErrorDescriber = describer;
            _AccountRepository = accountRepository;
        }


        public async Task<IdentityResult> CreateAsync(TUUsuario user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            { 
                throw new ArgumentNullException(nameof(user));
            }
            await _AccountRepository.AddAsync(user, cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TUUsuario user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                await _AccountRepository.RemoveAsync(user, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(_ErrorDescriber.ConcurrencyFailure());
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TUUsuario user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            try
            {
                await _AccountRepository.UpdateAsync(user, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(_ErrorDescriber.ConcurrencyFailure());
            }
            return IdentityResult.Success;
        }

        public Task<TUUsuario> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return _AccountRepository.FindByIdAsync(userId, cancellationToken);
        }

        public Task<TUUsuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return _AccountRepository.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(TUUsuario user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Nombre);
        }

        public Task<string> GetUserIdAsync(TUUsuario user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.IdUsuario);
        }

        public Task<string> GetUserNameAsync(TUUsuario user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Nombre);
        }

        public Task SetNormalizedUserNameAsync(TUUsuario user, string normalizedName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Nombre = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(TUUsuario user, string userName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Nombre = userName;
            return Task.CompletedTask;
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

        public async Task AddToRoleAsync(TUUsuario user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("ValueCannotBeNullOrEmpty", nameof(roleName));
            }
            var roleEntity = await _AccountRepository.FindRoleByNameAsync(roleName, cancellationToken);
            if (roleEntity == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "RoleNotFound", roleName));
            }
            user.RolId = roleEntity.IdRol;
            await _AccountRepository.UpdateUserRoleAsync(user, cancellationToken);
        }

        public Task RemoveFromRoleAsync(TUUsuario user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(TUUsuario user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult<IList<string>>(new List<string>() { user.RolId });
        }

        public async Task<bool> IsInRoleAsync(TUUsuario user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("ValueCannotBeNullOrEmpty", nameof(roleName));
            }
            var role = await _AccountRepository.FindRoleByNameAsync(roleName, cancellationToken);
            if (role != null)
            {
                return user.RolId == role.IdRol;
            }
            return false;
        }

        public async Task<IList<TUUsuario>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            var role = await _AccountRepository.FindRoleByNameAsync(roleName, cancellationToken);

            if (role != null)
            {
                return await _AccountRepository.GetByRolAsync(role.IdRol, cancellationToken);
            }

            return new List<TUUsuario>();
        }
    }
}
