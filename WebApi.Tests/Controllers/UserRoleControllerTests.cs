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
    public class UserRoleControllerTests
    {
        private readonly UserRoleController _userRoleController;
        private readonly Mock<IUserRoleService> _mockUserRoleService;

        public UserRoleControllerTests()
        {
            _mockUserRoleService = new Mock<IUserRoleService>();
            _userRoleController = new UserRoleController(_mockUserRoleService.Object);
        }

        #region GetAllUserRoles
        [Fact]
        public void GetAllUserRoles_WhenCalled_ReturnsOkResult()
        {
            var okResult = this._userRoleController.GetAllUserRoles() as ObjectResult;

            this._mockUserRoleService.Verify(m => m.GetAllUserRoles(), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetAllUserRoles_WhenCalled_ReturnsStatusCode500()
        {
            this._mockUserRoleService.Setup(m => m.GetAllUserRoles())
                .Throws(new Exception());

            var errorResult = this._userRoleController.GetAllUserRoles() as ObjectResult;

            this._mockUserRoleService.Verify(m => m.GetAllUserRoles(), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetUserRoleById
        [Fact]
        public void GetUserRoleById_WhenCalled_ReturnsOkResult()
        {
            this._mockUserRoleService.Setup(s => s.GetUserRoleById(It.IsAny<int>()))
                .Returns(this.CreateMockUserRole(1));

            var okResult = this._userRoleController.GetUserRoleById(It.IsAny<int>()) as ObjectResult;

            this._mockUserRoleService.Verify(s => s.GetUserRoleById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetUserRoleById_WhenCalled_ReturnsNotFound()
        {
            this._mockUserRoleService.Setup(s => s.GetUserRoleById(It.IsAny<int>()))
                .Returns(null as UserRole);

            var notFoundResult = this._userRoleController.GetUserRoleById(It.IsAny<int>()) as NotFoundResult;

            this._mockUserRoleService.Verify(s => s.GetUserRoleById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetUserRoleById_WhenCalled_ReturnsStatusCode500()
        {
            this._mockUserRoleService.Setup(s => s.GetUserRoleById(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._userRoleController.GetUserRoleById(It.IsAny<int>()) as ObjectResult;

            this._mockUserRoleService.Verify(s => s.GetUserRoleById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region CreateUserRole
        [Fact]
        public void CreateUserRole_WhenCalled_WithUserRoleNull_ReturnsBadRequestResult()
        {
            this._mockUserRoleService.Setup(s => s.CreateUserRole(It.IsAny<UserRole>()));
            this._mockUserRoleService.Setup(s => s.Save());

            var badRequest = this._userRoleController.CreateUserRole(null as UserRole) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateUserRole_WhenCalled_WithUserRoleWithIdNotNull_ReturnsBadRequestResult()
        {
            this._mockUserRoleService.Setup(s => s.CreateUserRole(It.IsAny<UserRole>()));
            this._mockUserRoleService.Setup(s => s.Save());

            var badRequest = this._userRoleController.CreateUserRole(this.CreateMockUserRole(2)) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateUserRole_WhenCalled_ReturnsOkResult()
        {
            this._mockUserRoleService.Setup(s => s.CreateUserRole(It.IsAny<UserRole>()))
                .Callback<UserRole>(t => t.Id = 1);
            this._mockUserRoleService.Setup(s => s.Save());

            var okResult = this._userRoleController.CreateUserRole(this.CreateMockUserRole(null)) as ObjectResult;

            this._mockUserRoleService.Verify(s => s.CreateUserRole(It.IsAny<UserRole>()), Times.Once);
            this._mockUserRoleService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [Fact]
        public void CreateUserRole_WhenCalled_ReturnsStatusCode500()
        {
            this._mockUserRoleService.Setup(s => s.CreateUserRole(It.IsAny<UserRole>()))
                .Throws(new Exception());
            this._mockUserRoleService.Setup(s => s.Save());

            var errorResult = this._userRoleController.CreateUserRole(this.CreateMockUserRole(null)) as ObjectResult;

            this._mockUserRoleService.Verify(s => s.CreateUserRole(It.IsAny<UserRole>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region UpdateUserRole
        [Fact]
        public void UpdateUserRole_WhenCalled_WithUserRoleNull_ReturnsBadRequestResult()
        {
            this._mockUserRoleService.Setup(s => s.UpdateUserRole(It.IsAny<UserRole>(), It.IsAny<UserRole>()));
            this._mockUserRoleService.Setup(s => s.GetUserRoleById(It.IsAny<int>()))
                .Returns(It.IsAny<UserRole>());
            this._mockUserRoleService.Setup(s => s.Save());

            var badRequest = this._userRoleController.UpdateUserRole(It.IsAny<int>(), null as UserRole) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateUserRole_WhenCalled_WithUserRoleWithIdNotFound_ReturnsBadRequestResult()
        {
            this._mockUserRoleService.Setup(s => s.UpdateUserRole(It.IsAny<UserRole>(), It.IsAny<UserRole>()));
            this._mockUserRoleService.Setup(s => s.GetUserRoleById(It.IsAny<int>()))
                .Returns(null as UserRole);
            this._mockUserRoleService.Setup(s => s.Save());

            var notFoundResult = this._userRoleController.UpdateUserRole(It.IsAny<int>(), this.CreateMockUserRole(2)) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateUserRole_WhenCalled_ReturnsOkResult()
        {
            this._mockUserRoleService.Setup(s => s.UpdateUserRole(It.IsAny<UserRole>(), It.IsAny<UserRole>()));
            this._mockUserRoleService.Setup(s => s.GetUserRoleById(It.IsAny<int>()))
                .Returns(this.CreateMockUserRole(1));
            this._mockUserRoleService.Setup(s => s.Save());

            var okResult = this._userRoleController.UpdateUserRole(It.IsAny<int>(), this.CreateMockUserRole(null)) as StatusCodeResult;

            this._mockUserRoleService.Verify(s => s.GetUserRoleById(It.IsAny<int>()), Times.Once);
            this._mockUserRoleService.Verify(s => s.UpdateUserRole(It.IsAny<UserRole>(), It.IsAny<UserRole>()), Times.Once);
            this._mockUserRoleService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void UpdateUserRole_WhenCalled_ReturnsStatusCode500()
        {
            this._mockUserRoleService.Setup(s => s.UpdateUserRole(It.IsAny<UserRole>(), It.IsAny<UserRole>()))
                .Throws(new Exception());
            this._mockUserRoleService.Setup(s => s.GetUserRoleById(It.IsAny<int>()))
                .Returns(this.CreateMockUserRole(1));
            this._mockUserRoleService.Setup(s => s.Save());

            var errorResult = this._userRoleController.UpdateUserRole(It.IsAny<int>(), this.CreateMockUserRole(null)) as ObjectResult;

            this._mockUserRoleService.Verify(s => s.GetUserRoleById(It.IsAny<int>()), Times.Once);
            this._mockUserRoleService.Verify(s => s.UpdateUserRole(It.IsAny<UserRole>(), It.IsAny<UserRole>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region DeleteUserRole
        [Fact]
        public void DeleteUserRole_WhenCalled_WithUserRoleWithIdNotFound_ReturnsNotFound()
        {
            this._mockUserRoleService.Setup(s => s.DeleteUserRole(It.IsAny<UserRole>()));
            this._mockUserRoleService.Setup(s => s.GetUserRoleById(It.IsAny<int>()))
                .Returns(null as UserRole);
            this._mockUserRoleService.Setup(s => s.Save());

            var notFoundResult = this._userRoleController.DeleteUserRole(It.IsAny<int>()) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void DeleteUserRole_WhenCalled_ReturnsOkResult()
        {
            this._mockUserRoleService.Setup(s => s.DeleteUserRole(It.IsAny<UserRole>()));
            this._mockUserRoleService.Setup(s => s.GetUserRoleById(It.IsAny<int>()))
                .Returns(this.CreateMockUserRole(1));
            this._mockUserRoleService.Setup(s => s.Save());

            var okResult = this._userRoleController.DeleteUserRole(It.IsAny<int>()) as StatusCodeResult;

            this._mockUserRoleService.Verify(s => s.GetUserRoleById(It.IsAny<int>()), Times.Once);
            this._mockUserRoleService.Verify(s => s.DeleteUserRole(It.IsAny<UserRole>()), Times.Once);
            this._mockUserRoleService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void DeleteUserRole_WhenCalled_ReturnsStatusCode500()
        {
            this._mockUserRoleService.Setup(s => s.DeleteUserRole(It.IsAny<UserRole>()))
                .Throws(new Exception());
            this._mockUserRoleService.Setup(s => s.GetUserRoleById(It.IsAny<int>()))
                .Returns(this.CreateMockUserRole(1));
            this._mockUserRoleService.Setup(s => s.Save());

            var errorResult = this._userRoleController.DeleteUserRole(It.IsAny<int>()) as ObjectResult;

            this._mockUserRoleService.Verify(s => s.GetUserRoleById(It.IsAny<int>()), Times.Once);
            this._mockUserRoleService.Verify(s => s.DeleteUserRole(It.IsAny<UserRole>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region Mock
        private UserRole CreateMockUserRole(int? id = null, int? roleId = null, int? userId = null)
        {
            return new UserRole()
            {
                Id = id,
                RoleId = roleId ?? 999,
                UserId = userId ?? 777
            };
        }
        #endregion
    }
}
