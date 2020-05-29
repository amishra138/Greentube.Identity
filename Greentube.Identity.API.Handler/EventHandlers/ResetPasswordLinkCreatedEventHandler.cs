using Greentube.Identity.Domain.Events;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Greentube.Identity.API.Handler.EventHandlers
{
    public class ResetPasswordLinkCreatedEventHandler : INotificationHandler<ResetPasswordLinkCreatedEvent>
    {
        public async Task Handle(ResetPasswordLinkCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                string[] lines = { "Dear Player, \n",
                                    "In order to login to your account so you can change your password click the following link: \n",
                                    notification.Link,
                                    "\n",
                                    "Note that this link is valid for 1 hour, after which it expires! \n",
                                    "Happy Playing! \n ", "Greentube Support Team"
                                 };

                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Files"));

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"Files\\resetPassword_{DateTime.Now.Ticks}.txt");
                await File.WriteAllLinesAsync(path, lines);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
