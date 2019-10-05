using BLL.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using WebApi.Controllers;
using Xunit;

namespace Tests.WebApi.Controllers
{
    public class RoleControllerTests
    {
        private readonly RoleController _roleController;
        private readonly Mock<IRoleService> _mockRoleService;

        public RoleControllerTests()
        {
            _mockRoleService = new Mock<IRoleService>();
            _roleController = new RoleController(_mockRoleService.Object);
        }

        #region GetAllRoles
        [Fact]
        public void GetAllRoles_WhenCalled_ReturnsOkResult()
        {
            var okResult = this._roleController.GetAllRoles() as ObjectResult;

            this._mockRoleService.Verify(m => m.GetAllRoles(), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetAllRoles_WhenCalled_ReturnsStatusCode500()
        {
            this._mockRoleService.Setup(m => m.GetAllRoles())
                .Throws(new Exception());

            var errorResult = this._roleController.GetAllRoles() as ObjectResult;

            this._mockRoleService.Verify(m => m.GetAllRoles(), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetRoleById
        [Fact]
        public void GetRoleById_WhenCalled_ReturnsOkResult()
        {
            this._mockRoleService.Setup(s => s.GetRoleById(It.IsAny<int>()))
                .Returns(this.CreateMockRole(1, "Test"));

            var okResult = this._roleController.GetRoleById(It.IsAny<int>()) as ObjectResult;

            this._mockRoleService.Verify(s => s.GetRoleById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetRoleById_WhenCalled_ReturnsNotFound()
        {
            this._mockRoleService.Setup(s => s.GetRoleById(It.IsAny<int>()))
                .Returns(null as Role);

            var notFoundResult = this._roleController.GetRoleById(It.IsAny<int>()) as NotFoundResult;

            this._mockRoleService.Verify(s => s.GetRoleById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetRoleById_WhenCalled_ReturnsStatusCode500()
        {
            this._mockRoleService.Setup(s => s.GetRoleById(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._roleController.GetRoleById(It.IsAny<int>()) as ObjectResult;

            this._mockRoleService.Verify(s => s.GetRoleById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region CreateRole
        [Fact]
        public void CreateRole_WhenCalled_WithRoleNull_ReturnsBadRequestResult()
        {
            this._mockRoleService.Setup(s => s.CreateRole(It.IsAny<Role>()));
            this._mockRoleService.Setup(s => s.Save());

            var badRequest = this._roleController.CreateRole(null as Role) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateRole_WhenCalled_WithRoleWithIdNotNull_ReturnsBadRequestResult()
        {
            this._mockRoleService.Setup(s => s.CreateRole(It.IsAny<Role>()));
            this._mockRoleService.Setup(s => s.Save());

            var badRequest = this._roleController.CreateRole(this.CreateMockRole(2)) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateRole_WhenCalled_ReturnsOkResult()
        {
            this._mockRoleService.Setup(s => s.CreateRole(It.IsAny<Role>()))
                .Callback<Role>(t => t.Id = 1);
            this._mockRoleService.Setup(s => s.Save());

            var okResult = this._roleController.CreateRole(this.CreateMockRole(null, "Test")) as ObjectResult;

            this._mockRoleService.Verify(s => s.CreateRole(It.IsAny<Role>()), Times.Once);
            this._mockRoleService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [Fact]
        public void CreateRole_WhenCalled_ReturnsStatusCode500()
        {
            this._mockRoleService.Setup(s => s.CreateRole(It.IsAny<Role>()))
                .Throws(new Exception());
            this._mockRoleService.Setup(s => s.Save());

            var errorResult = this._roleController.CreateRole(this.CreateMockRole(null, "Test")) as ObjectResult;

            this._mockRoleService.Verify(s => s.CreateRole(It.IsAny<Role>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region UpdateRole
        [Fact]
        public void UpdateRole_WhenCalled_WithRoleNull_ReturnsBadRequestResult()
        {
            this._mockRoleService.Setup(s => s.UpdateRole(It.IsAny<Role>(), It.IsAny<Role>()));
            this._mockRoleService.Setup(s => s.GetRoleById(It.IsAny<int>()))
                .Returns(It.IsAny<Role>());
            this._mockRoleService.Setup(s => s.Save());

            var badRequest = this._roleController.UpdateRole(It.IsAny<int>(), null as Role) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateRole_WhenCalled_WithRoleWithIdNotFound_ReturnsBadRequestResult()
        {
            this._mockRoleService.Setup(s => s.UpdateRole(It.IsAny<Role>(), It.IsAny<Role>()));
            this._mockRoleService.Setup(s => s.GetRoleById(It.IsAny<int>()))
                .Returns(null as Role);
            this._mockRoleService.Setup(s => s.Save());

            var notFoundResult = this._roleController.UpdateRole(It.IsAny<int>(), this.CreateMockRole(2)) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateRole_WhenCalled_ReturnsOkResult()
        {
            this._mockRoleService.Setup(s => s.UpdateRole(It.IsAny<Role>(), It.IsAny<Role>()));
            this._mockRoleService.Setup(s => s.GetRoleById(It.IsAny<int>()))
                .Returns(this.CreateMockRole(1));
            this._mockRoleService.Setup(s => s.Save());

            var okResult = this._roleController.UpdateRole(It.IsAny<int>(), this.CreateMockRole(null, "Test")) as StatusCodeResult;

            this._mockRoleService.Verify(s => s.GetRoleById(It.IsAny<int>()), Times.Once);
            this._mockRoleService.Verify(s => s.UpdateRole(It.IsAny<Role>(), It.IsAny<Role>()), Times.Once);
            this._mockRoleService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void UpdateRole_WhenCalled_ReturnsStatusCode500()
        {
            this._mockRoleService.Setup(s => s.UpdateRole(It.IsAny<Role>(), It.IsAny<Role>()))
                .Throws(new Exception());
            this._mockRoleService.Setup(s => s.GetRoleById(It.IsAny<int>()))
                .Returns(this.CreateMockRole(1));
            this._mockRoleService.Setup(s => s.Save());

            var errorResult = this._roleController.UpdateRole(It.IsAny<int>(), this.CreateMockRole(null, "Test")) as ObjectResult;

            this._mockRoleService.Verify(s => s.GetRoleById(It.IsAny<int>()), Times.Once);
            this._mockRoleService.Verify(s => s.UpdateRole(It.IsAny<Role>(), It.IsAny<Role>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region DeleteRole
        [Fact]
        public void DeleteRole_WhenCalled_WithRoleWithIdNotFound_ReturnsNotFound()
        {
            this._mockRoleService.Setup(s => s.DeleteRole(It.IsAny<Role>()));
            this._mockRoleService.Setup(s => s.GetRoleById(It.IsAny<int>()))
                .Returns(null as Role);
            this._mockRoleService.Setup(s => s.Save());

            var notFoundResult = this._roleController.DeleteRole(It.IsAny<int>()) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void DeleteRole_WhenCalled_ReturnsOkResult()
        {
            this._mockRoleService.Setup(s => s.DeleteRole(It.IsAny<Role>()));
            this._mockRoleService.Setup(s => s.GetRoleById(It.IsAny<int>()))
                .Returns(this.CreateMockRole(1));
            this._mockRoleService.Setup(s => s.Save());

            var okResult = this._roleController.DeleteRole(It.IsAny<int>()) as StatusCodeResult;

            this._mockRoleService.Verify(s => s.GetRoleById(It.IsAny<int>()), Times.Once);
            this._mockRoleService.Verify(s => s.DeleteRole(It.IsAny<Role>()), Times.Once);
            this._mockRoleService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void DeleteRole_WhenCalled_ReturnsStatusCode500()
        {
            this._mockRoleService.Setup(s => s.DeleteRole(It.IsAny<Role>()))
                .Throws(new Exception());
            this._mockRoleService.Setup(s => s.GetRoleById(It.IsAny<int>()))
                .Returns(this.CreateMockRole(1));
            this._mockRoleService.Setup(s => s.Save());

            var errorResult = this._roleController.DeleteRole(It.IsAny<int>()) as ObjectResult;

            this._mockRoleService.Verify(s => s.GetRoleById(It.IsAny<int>()), Times.Once);
            this._mockRoleService.Verify(s => s.DeleteRole(It.IsAny<Role>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region Mock
        private Role CreateMockRole(int? id = null, string code = null)
        {
            return new Role()
            {
                Id = id,
                Code = code,
                Label = String.Format("{0} of {1}", "Label", code)
            };
        }
        #endregion
    }
}
