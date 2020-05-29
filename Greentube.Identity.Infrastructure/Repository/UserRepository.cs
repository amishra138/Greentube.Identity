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
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public Task<IEnumerable<IdentityUser>> GetAll()
        {
            //just to wrap into a task
            return Task.FromResult(_userManager.Users.AsEnumerable());
        }

        public Task<IdentityUser> GetByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> ResetPassword(IdentityUser user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result == null ? false : result.Succeeded;
        }
        public Task<string> GeneratePasswordResetTokenAsync(IdentityUser user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public Task<bool> VerifyUserTokenAsync(IdentityUser user, string token)
        {
            return _userManager.VerifyUserTokenAsync(user, "Default", "ResetPassword", token);
        }

        public async Task<bool> ResetPasswordAsync(IdentityUser user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result == null ? false : result.Succeeded;
        }
    }
}
