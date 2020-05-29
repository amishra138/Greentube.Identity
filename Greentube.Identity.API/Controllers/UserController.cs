using Greentube.Identity.API.Handler.Queries;
using Greentube.Identity.Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Greentube.Identity.API.Controllers
{
    /// <summary>
    /// User Identity controller
    /// </summary>
    public class UserController : ApiBaseController<UserController>
    {
        private readonly IUserQuery _userQuery;
        private CancellationTokenSource _cancellationToken;

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="userQuery"></param>
        public UserController(IUserQuery userQuery)
        {
            _userQuery = userQuery ?? throw new ArgumentNullException(nameof(userQuery));
        }

        /// <summary>
        /// To get all registered users
        /// </summary>
        /// <returns>Return list of users</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userQuery.GetAllUsers();

            if (result == null)
                NoContent();

            return Ok(result);
        }

        /// <summary>
        /// To generate a forgot password link, which will be sent on user's registered email address
        /// </summary>
        /// <param name="request">User email</param>
        /// <returns>Password reset link</returns>
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordRequest request)
        {
            _cancellationToken = new CancellationTokenSource();

            var resetLink = await Mediator.Send(request, _cancellationToken.Token);

            return Ok(new { message = "Password reset email has been sent to your registered email, please check", link = resetLink });
        }

        /// <summary>
        /// To reset password via password reset link sent on registered email
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="token">Password reset token</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TempLogin(string email, string token)
        {
            _cancellationToken = new CancellationTokenSource();

            var result = await Mediator.Send(new ResetPasswordRequest(email, token), _cancellationToken.Token);

            return Ok(result);
        }
    }
}
