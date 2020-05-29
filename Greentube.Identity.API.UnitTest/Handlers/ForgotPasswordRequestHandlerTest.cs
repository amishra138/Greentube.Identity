using Greentube.Identity.API.Handler.RequestHandlers;
using Greentube.Identity.Domain.Events;
using Greentube.Identity.Domain.Interfaces;
using Greentube.Identity.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Greentube.Identity.API.UnitTest.Handlers
{
    [TestClass]
    public class ForgotPasswordRequestHandlerTest
    {
        private readonly Mock<IUserRepository> _repository = new Mock<IUserRepository>();
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        private ForgotPasswordRequestHandler _forgotPasswordRequestHandler;

        [TestInitialize]
        public void Initialize()
        {
            _forgotPasswordRequestHandler = new ForgotPasswordRequestHandler(_repository.Object, _mediator.Object, _httpContextAccessor.Object);
        }

        [TestMethod]
        public void Test_When_User_NotFound()
        {
            //Arrange
            IdentityUser user = null;
            _repository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            //Act
            var result = _forgotPasswordRequestHandler.Handle(new ForgotPasswordRequest() { Email = "arvind.mishra138@gmail.com" }, default).GetAwaiter().GetResult();

            //Assert
            Assert.AreNotEqual(string.Empty, result);
            Assert.AreEqual("User not found", result);
        }

        [TestMethod]
        public void Test_When_User_Found()
        {
            //Arrange
            IdentityUser user = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "arvind",
                Email = "arvind.mishra138@gmail.com"
            };

            _repository
                .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));

            _repository
                .Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<IdentityUser>()))
                .Returns(Task.FromResult(Guid.NewGuid().ToString()));

            _httpContextAccessor
                .Setup(_ => _.HttpContext)
                .Returns(GetDefaultHttpContext());

            _mediator
                .Setup(x => x.Publish(It.IsAny<ResetPasswordLinkCreatedEvent>(), default))
                .Returns(Task.CompletedTask);

            //Act
            var result = _forgotPasswordRequestHandler.Handle(new ForgotPasswordRequest() { Email = "arvind.mishra138@gmail.com" }, default).GetAwaiter().GetResult();

            //Assert
            Assert.AreNotEqual(string.Empty, result);
            Assert.IsTrue(result.Contains("email"));
            Assert.IsTrue(result.Contains("token"));
        }

        private DefaultHttpContext GetDefaultHttpContext()
        {
            var context = new DefaultHttpContext();
            context.Request.Scheme = "http";
            context.Request.Host = new HostString("localhost", 55633);
            return context;
        }
    }
}
