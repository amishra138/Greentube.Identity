using MediatR;

namespace Greentube.Identity.Domain.Requests
{
    public class ResetPasswordRequest : IRequest<string>
    {
        public string Email { get; private set; }

        public string Token { get; private set; }

        public ResetPasswordRequest(string email, string token)
        {
            Email = email;
            Token = token;
        }
    }
}
