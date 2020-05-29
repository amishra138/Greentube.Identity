using Greentube.Identity.Domain.Entities;
using Greentube.Identity.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greentube.Identity.Infrastructure.Repository
{
    /// <summary>
    /// User repository contract's implementation
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private bool _disposed;
        private readonly UserManager<UserEntity> _userManager;

        public UserRepository(UserManager<UserEntity> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public Task<IEnumerable<UserEntity>> GetAll()
        {
            //just to wrap into a task
            return Task.FromResult(_userManager.Users.AsEnumerable());
        }

        public Task<UserEntity> GetByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> ResetPassword(UserEntity user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result == null ? false : result.Succeeded;
        }

        public void Dispose()
        {
            Dispose(true);

            //To inform GC that this object was cleaned up fully, so that it wouldn't waste time cleaning it again
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
