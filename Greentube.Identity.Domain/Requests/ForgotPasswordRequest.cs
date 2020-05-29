using MediatR;

namespace Greentube.Identity.Domain.Requests
{
    /// <summary>
    /// Client forgot password request
    /// </summary>
    public class ForgotPasswordRequest : IRequest<string>
    {
        /// <summary>
        /// User unique email address
        /// </summary>
        public string Email { get; set; }
    }
}
