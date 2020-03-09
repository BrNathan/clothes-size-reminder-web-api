using BLL.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using WebApi.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _mockUserService;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }

        #region GetAllUsers
        [Fact]
        public void GetAllUsers_WhenCalled_ReturnsOkResult()
        {
            var okResult = this._userController.GetAllUsers() as ObjectResult;

            this._mockUserService.Verify(m => m.GetAllUsers(), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetAllUsers_WhenCalled_ReturnsStatusCode500()
        {
            this._mockUserService.Setup(m => m.GetAllUsers())
                .Throws(new Exception());

            var errorResult = this._userController.GetAllUsers() as ObjectResult;

            this._mockUserService.Verify(m => m.GetAllUsers(), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetUserById
        [Fact]
        public void GetUserById_WhenCalled_ReturnsOkResult()
        {
            this._mockUserService.Setup(s => s.GetUserById(It.IsAny<int>()))
                .Returns(this.CreateMockUser(1, "Test"));

            var okResult = this._userController.GetUserById(It.IsAny<int>()) as ObjectResult;

            this._mockUserService.Verify(s => s.GetUserById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetUserById_WhenCalled_ReturnsNotFound()
        {
            this._mockUserService.Setup(s => s.GetUserById(It.IsAny<int>()))
                .Returns(null as User);

            var notFoundResult = this._userController.GetUserById(It.IsAny<int>()) as NotFoundResult;

            this._mockUserService.Verify(s => s.GetUserById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetUserById_WhenCalled_ReturnsStatusCode500()
        {
            this._mockUserService.Setup(s => s.GetUserById(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._userController.GetUserById(It.IsAny<int>()) as ObjectResult;

            this._mockUserService.Verify(s => s.GetUserById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region CreateUser
        [Fact]
        public void CreateUser_WhenCalled_WithUserNull_ReturnsBadRequestResult()
        {
            this._mockUserService.Setup(s => s.CreateUser(It.IsAny<User>()));
            this._mockUserService.Setup(s => s.Save());

            var badRequest = this._userController.CreateUser(null as User) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateUser_WhenCalled_WithUserWithIdNotNull_ReturnsBadRequestResult()
        {
            this._mockUserService.Setup(s => s.CreateUser(It.IsAny<User>()));
            this._mockUserService.Setup(s => s.Save());

            var badRequest = this._userController.CreateUser(this.CreateMockUser(2)) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateUser_WhenCalled_ReturnsOkResult()
        {
            this._mockUserService.Setup(s => s.CreateUser(It.IsAny<User>()))
                .Callback<User>(t => t.Id = 1);
            this._mockUserService.Setup(s => s.Save());

            var okResult = this._userController.CreateUser(this.CreateMockUser(null, "Test")) as ObjectResult;

            this._mockUserService.Verify(s => s.CreateUser(It.IsAny<User>()), Times.Once);
            this._mockUserService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [Fact]
        public void CreateUser_WhenCalled_ReturnsStatusCode500()
        {
            this._mockUserService.Setup(s => s.CreateUser(It.IsAny<User>()))
                .Throws(new Exception());
            this._mockUserService.Setup(s => s.Save());

            var errorResult = this._userController.CreateUser(this.CreateMockUser(null, "Test")) as ObjectResult;

            this._mockUserService.Verify(s => s.CreateUser(It.IsAny<User>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region UpdateUser
        [Fact]
        public void UpdateUser_WhenCalled_WithUserNull_ReturnsBadRequestResult()
        {
            this._mockUserService.Setup(s => s.UpdateUser(It.IsAny<User>(), It.IsAny<User>()));
            this._mockUserService.Setup(s => s.GetUserById(It.IsAny<int>()))
                .Returns(It.IsAny<User>());
            this._mockUserService.Setup(s => s.Save());

            var badRequest = this._userController.UpdateUser(It.IsAny<int>(), null as User) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateUser_WhenCalled_WithUserWithIdNotFound_ReturnsBadRequestResult()
        {
            this._mockUserService.Setup(s => s.UpdateUser(It.IsAny<User>(), It.IsAny<User>()));
            this._mockUserService.Setup(s => s.GetUserById(It.IsAny<int>()))
                .Returns(null as User);
            this._mockUserService.Setup(s => s.Save());

            var notFoundResult = this._userController.UpdateUser(It.IsAny<int>(), this.CreateMockUser(2)) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateUser_WhenCalled_ReturnsOkResult()
        {
            this._mockUserService.Setup(s => s.UpdateUser(It.IsAny<User>(), It.IsAny<User>()));
            this._mockUserService.Setup(s => s.GetUserById(It.IsAny<int>()))
                .Returns(this.CreateMockUser(1));
            this._mockUserService.Setup(s => s.Save());

            var okResult = this._userController.UpdateUser(It.IsAny<int>(), this.CreateMockUser(null, "Test")) as StatusCodeResult;

            this._mockUserService.Verify(s => s.GetUserById(It.IsAny<int>()), Times.Once);
            this._mockUserService.Verify(s => s.UpdateUser(It.IsAny<User>(), It.IsAny<User>()), Times.Once);
            this._mockUserService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void UpdateUser_WhenCalled_ReturnsStatusCode500()
        {
            this._mockUserService.Setup(s => s.UpdateUser(It.IsAny<User>(), It.IsAny<User>()))
                .Throws(new Exception());
            this._mockUserService.Setup(s => s.GetUserById(It.IsAny<int>()))
                .Returns(this.CreateMockUser(1));
            this._mockUserService.Setup(s => s.Save());

            var errorResult = this._userController.UpdateUser(It.IsAny<int>(), this.CreateMockUser(null, "Test")) as ObjectResult;

            this._mockUserService.Verify(s => s.GetUserById(It.IsAny<int>()), Times.Once);
            this._mockUserService.Verify(s => s.UpdateUser(It.IsAny<User>(), It.IsAny<User>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region DeleteUser
        [Fact]
        public void DeleteUser_WhenCalled_WithUserWithIdNotFound_ReturnsNotFound()
        {
            this._mockUserService.Setup(s => s.DeleteUser(It.IsAny<User>()));
            this._mockUserService.Setup(s => s.GetUserById(It.IsAny<int>()))
                .Returns(null as User);
            this._mockUserService.Setup(s => s.Save());

            var notFoundResult = this._userController.DeleteUser(It.IsAny<int>()) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void DeleteUser_WhenCalled_ReturnsOkResult()
        {
            this._mockUserService.Setup(s => s.DeleteUser(It.IsAny<User>()));
            this._mockUserService.Setup(s => s.GetUserById(It.IsAny<int>()))
                .Returns(this.CreateMockUser(1));
            this._mockUserService.Setup(s => s.Save());

            var okResult = this._userController.DeleteUser(It.IsAny<int>()) as StatusCodeResult;

            this._mockUserService.Verify(s => s.GetUserById(It.IsAny<int>()), Times.Once);
            this._mockUserService.Verify(s => s.DeleteUser(It.IsAny<User>()), Times.Once);
            this._mockUserService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void DeleteUser_WhenCalled_ReturnsStatusCode500()
        {
            this._mockUserService.Setup(s => s.DeleteUser(It.IsAny<User>()))
                .Throws(new Exception());
            this._mockUserService.Setup(s => s.GetUserById(It.IsAny<int>()))
                .Returns(this.CreateMockUser(1));
            this._mockUserService.Setup(s => s.Save());

            var errorResult = this._userController.DeleteUser(It.IsAny<int>()) as ObjectResult;

            this._mockUserService.Verify(s => s.GetUserById(It.IsAny<int>()), Times.Once);
            this._mockUserService.Verify(s => s.DeleteUser(It.IsAny<User>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region Mock
        private User CreateMockUser(int? id = null, string firstName = "firstName", string lastName = "lastName", string email = "email")
        {
            return new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                GenderId = 2,
                Password = "empty password"
            };
        }
        #endregion
    }
}
