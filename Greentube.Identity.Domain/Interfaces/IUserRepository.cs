using Greentube.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greentube.Identity.Domain.Interfaces
{
    /// <summary>
    /// User repository contracts
    /// </summary>
    public interface IUserRepository : IDisposable
    {
        /// <summary>
        /// To get all users from database
        /// </summary>
        /// <returns>IEnumberable collection of system users</returns>
        Task<IEnumerable<UserEntity>> GetAll();

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">User email address</param>
        /// <returns>Specific user based on query parameter</returns>
        Task<UserEntity> GetByEmailAsync(string email);

        /// <summary>
        /// To reset system user password
        /// </summary>
        /// <param name="user">User of which reset password has requested</param>
        /// <param name="token">System generated token to identify valid user</param>
        /// <param name="newPassword">New password matching system password criteria</param>
        /// <returns>True, in case password has been reset successfully, otherwise false</returns>
        Task<bool> ResetPassword(UserEntity user, string token, string newPassword);
    }
}
