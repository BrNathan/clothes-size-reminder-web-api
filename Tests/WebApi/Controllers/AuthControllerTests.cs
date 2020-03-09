using BLL.Interfaces;
using Entities.ExtendedModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;
using WebApi.Model;
using Xunit;

namespace Tests.WebApi.Controllers
{
    public class AuthControllerTests
    {

        private readonly AuthController _authController;
        private readonly Mock<IAuthService> _mockAuthService;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _authController = new AuthController(_mockAuthService.Object);
        }

        #region Login
        [Fact]
        public void Login_WhenCalled_ReturnsBadRequest()
        {
            //Arrange
            LoginModel model = new LoginModel()
            {
                Email = It.IsAny<string>(),
                PasswordHash = It.IsAny<string>()
            };
            _mockAuthService.Setup(s => s.Authenticate(model.Email, model.PasswordHash))
                .Returns(() => null);

            //Act
            ObjectResult badRequestResult = _authController.Login(model) as ObjectResult;

            //Assert
            _mockAuthService.Verify(s => s.Authenticate(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public void Login_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            LoginModel model = new LoginModel()
            {
                Email = It.IsAny<string>(),
                PasswordHash = It.IsAny<string>()
            };
            UserAuthenticate userAuthenticateMock = new UserAuthenticate();
            _mockAuthService.Setup(s => s.Authenticate(model.Email, model.PasswordHash))
                .Returns(() => userAuthenticateMock);

            //Act
            ObjectResult okResult = _authController.Login(model) as ObjectResult;

            //Assert
            _mockAuthService.Verify(s => s.Authenticate(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        #endregion
    }
}
