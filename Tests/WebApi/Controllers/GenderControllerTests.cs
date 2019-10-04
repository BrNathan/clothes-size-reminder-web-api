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
    public class GenderControllerTests
    {
        private readonly GenderController _genderController;
        private readonly Mock<IGenderService> _mockGenderService;

        public GenderControllerTests()
        {
            _mockGenderService = new Mock<IGenderService>();
            _genderController = new GenderController(_mockGenderService.Object);
        }

        #region GetAllGenders
        [Fact]
        public void GetAllGenders_WhenCalled_ReturnsOkResult()
        {
            var okResult = this._genderController.GetAllGenders() as ObjectResult;

            this._mockGenderService.Verify(m => m.GetAllGenders(), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetAllGenders_WhenCalled_ReturnsStatusCode500()
        {
            this._mockGenderService.Setup(m => m.GetAllGenders())
                .Throws(new Exception());

            var errorResult = this._genderController.GetAllGenders() as ObjectResult;

            this._mockGenderService.Verify(m => m.GetAllGenders(), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetGenderById
        [Fact]
        public void GetGenderById_WhenCalled_ReturnsOkResult()
        {
            this._mockGenderService.Setup(s => s.GetGenderById(It.IsAny<int>()))
                .Returns(this.CreateMockGender(1));

            var okResult = this._genderController.GetGenderById(It.IsAny<int>()) as ObjectResult;

            this._mockGenderService.Verify(s => s.GetGenderById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetGenderById_WhenCalled_ReturnsNotFound()
        {
            this._mockGenderService.Setup(s => s.GetGenderById(It.IsAny<int>()))
                .Returns(null as Gender);

            var notFoundResult = this._genderController.GetGenderById(It.IsAny<int>()) as NotFoundResult;

            this._mockGenderService.Verify(s => s.GetGenderById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetGenderById_WhenCalled_ReturnsStatusCode500()
        {
            this._mockGenderService.Setup(s => s.GetGenderById(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._genderController.GetGenderById(It.IsAny<int>()) as ObjectResult;

            this._mockGenderService.Verify(s => s.GetGenderById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region CreateGender
        [Fact]
        public void CreateGender_WhenCalled_WithGenderNull_ReturnsBadRequestResult()
        {
            this._mockGenderService.Setup(s => s.CreateGender(It.IsAny<Gender>()));
            this._mockGenderService.Setup(s => s.Save());

            var badRequest = this._genderController.CreateGender(null as Gender) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateGender_WhenCalled_WithGenderWithIdNotNull_ReturnsBadRequestResult()
        {
            this._mockGenderService.Setup(s => s.CreateGender(It.IsAny<Gender>()));
            this._mockGenderService.Setup(s => s.Save());

            var badRequest = this._genderController.CreateGender(this.CreateMockGender(2)) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateGender_WhenCalled_ReturnsOkResult()
        {
            this._mockGenderService.Setup(s => s.CreateGender(It.IsAny<Gender>()))
                .Callback<Gender>(t => t.Id = 1);
            this._mockGenderService.Setup(s => s.Save());

            var okResult = this._genderController.CreateGender(this.CreateMockGender(null)) as ObjectResult;

            this._mockGenderService.Verify(s => s.CreateGender(It.IsAny<Gender>()), Times.Once);
            this._mockGenderService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [Fact]
        public void CreateGender_WhenCalled_ReturnsStatusCode500()
        {
            this._mockGenderService.Setup(s => s.CreateGender(It.IsAny<Gender>()))
                .Throws(new Exception());
            this._mockGenderService.Setup(s => s.Save());

            var errorResult = this._genderController.CreateGender(this.CreateMockGender(null)) as ObjectResult;

            this._mockGenderService.Verify(s => s.CreateGender(It.IsAny<Gender>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region UpdateGender
        [Fact]
        public void UpdateGender_WhenCalled_WithGenderNull_ReturnsBadRequestResult()
        {
            this._mockGenderService.Setup(s => s.UpdateGender(It.IsAny<Gender>(), It.IsAny<Gender>()));
            this._mockGenderService.Setup(s => s.GetGenderById(It.IsAny<int>()))
                .Returns(It.IsAny<Gender>());
            this._mockGenderService.Setup(s => s.Save());

            var badRequest = this._genderController.UpdateGender(It.IsAny<int>(), null as Gender) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateGender_WhenCalled_WithGenderWithIdNotFound_ReturnsBadRequestResult()
        {
            this._mockGenderService.Setup(s => s.UpdateGender(It.IsAny<Gender>(), It.IsAny<Gender>()));
            this._mockGenderService.Setup(s => s.GetGenderById(It.IsAny<int>()))
                .Returns(null as Gender);
            this._mockGenderService.Setup(s => s.Save());

            var notFoundResult = this._genderController.UpdateGender(It.IsAny<int>(), this.CreateMockGender(2)) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateGender_WhenCalled_ReturnsOkResult()
        {
            this._mockGenderService.Setup(s => s.UpdateGender(It.IsAny<Gender>(), It.IsAny<Gender>()));
            this._mockGenderService.Setup(s => s.GetGenderById(It.IsAny<int>()))
                .Returns(this.CreateMockGender(1));
            this._mockGenderService.Setup(s => s.Save());

            var okResult = this._genderController.UpdateGender(It.IsAny<int>(), this.CreateMockGender(null)) as StatusCodeResult;

            this._mockGenderService.Verify(s => s.GetGenderById(It.IsAny<int>()), Times.Once);
            this._mockGenderService.Verify(s => s.UpdateGender(It.IsAny<Gender>(), It.IsAny<Gender>()), Times.Once);
            this._mockGenderService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void UpdateGender_WhenCalled_ReturnsStatusCode500()
        {
            this._mockGenderService.Setup(s => s.UpdateGender(It.IsAny<Gender>(), It.IsAny<Gender>()))
                .Throws(new Exception());
            this._mockGenderService.Setup(s => s.GetGenderById(It.IsAny<int>()))
                .Returns(this.CreateMockGender(1));
            this._mockGenderService.Setup(s => s.Save());

            var errorResult = this._genderController.UpdateGender(It.IsAny<int>(), this.CreateMockGender(null)) as ObjectResult;

            this._mockGenderService.Verify(s => s.GetGenderById(It.IsAny<int>()), Times.Once);
            this._mockGenderService.Verify(s => s.UpdateGender(It.IsAny<Gender>(), It.IsAny<Gender>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region DeleteGender
        [Fact]
        public void DeleteGender_WhenCalled_WithGenderWithIdNotFound_ReturnsNotFound()
        {
            this._mockGenderService.Setup(s => s.DeleteGender(It.IsAny<Gender>()));
            this._mockGenderService.Setup(s => s.GetGenderById(It.IsAny<int>()))
                .Returns(null as Gender);
            this._mockGenderService.Setup(s => s.Save());

            var notFoundResult = this._genderController.DeleteGender(It.IsAny<int>()) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void DeleteGender_WhenCalled_ReturnsOkResult()
        {
            this._mockGenderService.Setup(s => s.DeleteGender(It.IsAny<Gender>()));
            this._mockGenderService.Setup(s => s.GetGenderById(It.IsAny<int>()))
                .Returns(this.CreateMockGender(1));
            this._mockGenderService.Setup(s => s.Save());

            var okResult = this._genderController.DeleteGender(It.IsAny<int>()) as StatusCodeResult;

            this._mockGenderService.Verify(s => s.GetGenderById(It.IsAny<int>()), Times.Once);
            this._mockGenderService.Verify(s => s.DeleteGender(It.IsAny<Gender>()), Times.Once);
            this._mockGenderService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void DeleteGender_WhenCalled_ReturnsStatusCode500()
        {
            this._mockGenderService.Setup(s => s.DeleteGender(It.IsAny<Gender>()))
                .Throws(new Exception());
            this._mockGenderService.Setup(s => s.GetGenderById(It.IsAny<int>()))
                .Returns(this.CreateMockGender(1));
            this._mockGenderService.Setup(s => s.Save());

            var errorResult = this._genderController.DeleteGender(It.IsAny<int>()) as ObjectResult;

            this._mockGenderService.Verify(s => s.GetGenderById(It.IsAny<int>()), Times.Once);
            this._mockGenderService.Verify(s => s.DeleteGender(It.IsAny<Gender>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region Mock
        private Gender CreateMockGender(int? id = null, string code = "code", string label = "label")
        {
            return new Gender()
            {
                Id = id,
                Code = code,
                Label = label
            };
        }
        #endregion
    }
}
