using Greentube.Identity.Domain.Events;
using Greentube.Identity.Domain.Interfaces;
using Greentube.Identity.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Greentube.Identity.API.Handler.RequestHandlers
{
    public class ForgotPasswordRequestHandler : IRequestHandler<ForgotPasswordRequest, string>
    {
        private readonly IUserRepository _repository;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ForgotPasswordRequestHandler(IUserRepository repository, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<string> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string result = string.Empty;

                var user = await _repository.GetByEmailAsync(request.Email);

                if (user == null)
                    return "User not found";

                var code = await _repository.GeneratePasswordResetTokenAsync(user);

                //code should be UrlEncoded to transfer over http
                result = GenerateLink(request.Email, HttpUtility.UrlEncode(code));

                //send notification
                _ = _mediator.Publish(new ResetPasswordLinkCreatedEvent(request.Email, result));

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GenerateLink(string email, string token)
        {
            return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/user/templogin?email={email}&token={token}";
        }
    }
}
