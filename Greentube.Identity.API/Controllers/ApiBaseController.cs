using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Greentube.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiBaseController<T> : ControllerBase where T : class
    {
        private IMediator _mediator;
        private ILogger<T> _logger;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();
    }
}
