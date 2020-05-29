using Greentube.Identity.API.Handler.RequestHandlers;
using Greentube.Identity.Domain.Interfaces;
using Greentube.Identity.Domain.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Greentube.Identity.API.UnitTest.Handlers
{
    [TestClass]
    public class ResetPasswordRequestHandlerTest
    {
        private readonly Mock<IUserRepository> _repository = new Mock<IUserRepository>();

        private ResetPasswordRequestHandler _requestHandler;

        [TestInitialize]
        public void Initialize()
        {
            _requestHandler = new ResetPasswordRequestHandler(_repository.Object);
        }

        [TestMethod]
        public void Test_When_User_NotFound()
        {
            //Arrange
            IdentityUser user = null;
            _repository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            //Act
            var result = _requestHandler.Handle(new ResetPasswordRequest("arvind.mishra138@gmail.com", Guid.NewGuid().ToString()), default).GetAwaiter().GetResult();

            //Assert
            Assert.AreNotEqual(string.Empty, result);
            Assert.AreEqual("User not found", result);
        }

        [TestMethod]
        public void Test_When_Toke_IsInvalid()
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
                .Setup(x => x.VerifyUserTokenAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            //Act
            var result = _requestHandler.Handle(new ResetPasswordRequest("arvind.mishra138@gmail.com", Guid.NewGuid().ToString()), default).GetAwaiter().GetResult();

            //Assert
            Assert.AreNotEqual(string.Empty, result);
            Assert.AreEqual("Link has expired, please generate a new link", result);
        }

        [TestMethod]
        public void Test_When_Toke_IsValid()
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
                .Setup(x => x.VerifyUserTokenAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            _repository
              .Setup(x => x.ResetPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>(),It.IsAny<string>()))
              .Returns(Task.FromResult(true));

            //Act
            var result = _requestHandler.Handle(new ResetPasswordRequest("arvind.mishra138@gmail.com", Guid.NewGuid().ToString()), default).GetAwaiter().GetResult();

            //Assert
            Assert.AreNotEqual(string.Empty, result);
            Assert.AreEqual("Password has been reset successfully", result);
        }
    }
}
