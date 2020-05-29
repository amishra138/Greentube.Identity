using Greentube.Identity.Domain.Interfaces;
using Greentube.Identity.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Greentube.Identity.API.Handler.RequestHandlers
{
    public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, string>
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public ResetPasswordRequestHandler(IUserRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userManager = userManager;
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

                var code = System.Web.HttpUtility.UrlDecode(request.Token);
                //var isTokenValid = await _repository.VerifyUserTokenAsync(user, code);

                var isTokenValid = await _userManager.VerifyUserTokenAsync(user, "Default", "ResetPassword", code);

                if (isTokenValid)
                {
                    if (user == null)
                        return "Bad username and password";

                    var result = await _userManager.ResetPasswordAsync(user, code, "Reset@123");

                    return result.Succeeded ? "Password has been reset successfully" : "Something went wrong";
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
