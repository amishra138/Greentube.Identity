using MediatR;

namespace Greentube.Identity.Domain.Events
{
    /// <summary>
    /// Domain event notification, once password reset link has created
    /// </summary>
    public class ResetPasswordLinkCreatedEvent : INotification
    {
        public string Email { get; private set; }

        public string Link { get; private set; }

        public ResetPasswordLinkCreatedEvent(string email, string link)
        {
            Email = email;
            Link = link;
        }
    }
}
