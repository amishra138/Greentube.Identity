using Greentube.Identity.Domain.Interfaces;
using Greentube.Identity.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greentube.Identity.API.Handler.Queries
{
    public class UserQuery : IUserQuery
    {
        IUserRepository _repository;
        public UserQuery(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            List<UserModel> _users = new List<UserModel>();
            var users = await _repository.GetAll();

            foreach (var user in users)
            {
                _users.Add(new UserModel()
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    UserName = user.UserName,
                });
            }

            return _users;
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var result = await _repository.GetByEmailAsync(email);

            if (result == null)
                return null;

            return new UserModel()
            {
                Email = result.Email,
                UserName = result.UserName,
            };
        }
    }
}
