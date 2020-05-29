using Greentube.Identity.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greentube.Identity.Domain.Interfaces
{
    /// <summary>
    /// To query over system users
    /// </summary>
    public interface IUserQuery
    {
        Task<UserModel> GetUserByEmailAsync(string email);

        Task<IEnumerable<UserModel>> GetAllUsers();
    }
}
