using Greentube.Identity.Domain.Interfaces;
using Greentube.Identity.Domain.Requests;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Greentube.Identity.API.Handler.RequestHandlers
{
    public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, string>
    {
        private readonly IUserRepository _repository;

        public ResetPasswordRequestHandler(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<string> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _repository.GetByEmailAsync(request.Email);

                if (user == null)
                {
                    return "User not found";
                }

                var isTokenValid = await _repository.VerifyUserTokenAsync(user, request.Token);

                if (isTokenValid)
                {
                    var result = await _repository.ResetPasswordAsync(user, request.Token, "Reset@123");

                    return result ? "Password has been reset successfully" : "Something went wrong";
                }
                else
                {
                    return "Link has expired, please generate a new link";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
