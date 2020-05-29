using Greentube.Identity.Domain.Interfaces;
using Greentube.Identity.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            Logger.LogInformation("calling user get all function");

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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordRequest request)
        {
            Logger.LogInformation("calling user forgot password action");

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
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> TempLogin(string email, string token)
        {
            Logger.LogInformation("calling user temp login action");

            _cancellationToken = new CancellationTokenSource();

            var result = await Mediator.Send(new ResetPasswordRequest(email, token), _cancellationToken.Token);

            if (result == "Password has been reset successfully")
            {
                return RedirectToAction("LoginByEmailAddress", new { email });
            }
            return Ok(result);
        }

        /// <summary>
        /// Login by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult LoginByEmailAddress(string email)
        {
            Logger.LogInformation("calling user Login By Email Address action");

            return Ok(new { email, message = "Successfully login" });
        }
    }
}
