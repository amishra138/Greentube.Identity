using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greentube.Identity.Domain.Interfaces
{
    /// <summary>
    /// User repository contracts
    /// </summary>
    public interface IUserRepository 
    {
        /// <summary>
        /// To get all users from database
        /// </summary>
        /// <returns>IEnumberable collection of system users</returns>
        Task<IEnumerable<IdentityUser>> GetAll();

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">User email address</param>
        /// <returns>Specific user based on query parameter</returns>
        Task<IdentityUser> GetByEmailAsync(string email);

        /// <summary>
        /// To reset system user password
        /// </summary>
        /// <param name="user">User of which reset password has requested</param>
        /// <param name="token">System generated token to identify valid user</param>
        /// <param name="newPassword">New password matching system password criteria</param>
        /// <returns>True, in case password has been reset successfully, otherwise false</returns>
        Task<bool> ResetPassword(IdentityUser user, string token, string newPassword);

        /// <summary>
        /// To generate unique password reset code
        /// </summary>
        /// <param name="user">User for which code has requested</param>
        /// <returns>Unique hash code</returns>
        Task<string> GeneratePasswordResetTokenAsync(IdentityUser user);

        /// <summary>
        /// To verify password reset generated token
        /// </summary>
        /// <param name="user">System user for which token has generated</param>
        /// <param name="token">Password reset token</param>
        /// <returns></returns>
        Task<bool> VerifyUserTokenAsync(IdentityUser user, string token);

        /// <summary>
        /// Reset user's password
        /// </summary>
        /// <param name="user">System user for which token has generated</param>
        /// <param name="token">Password reset token</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        Task<bool> ResetPasswordAsync(IdentityUser user, string token, string newPassword);
    }
}
