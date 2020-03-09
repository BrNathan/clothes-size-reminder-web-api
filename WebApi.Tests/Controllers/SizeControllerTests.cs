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
    public class SizeControllerTests
    {
        private readonly SizeController _sizeController;
        private readonly Mock<ISizeService> _mockSizeService;

        public SizeControllerTests()
        {
            _mockSizeService = new Mock<ISizeService>();
            _sizeController = new SizeController(_mockSizeService.Object);
        }

        #region GetAllSizes
        [Fact]
        public void GetAllSizes_WhenCalled_ReturnsOkResult()
        {
            var okResult = this._sizeController.GetAllSizes() as ObjectResult;

            this._mockSizeService.Verify(m => m.GetAllSizes(), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetAllSizes_WhenCalled_ReturnsStatusCode500()
        {
            this._mockSizeService.Setup(m => m.GetAllSizes())
                .Throws(new Exception());

            var errorResult = this._sizeController.GetAllSizes() as ObjectResult;

            this._mockSizeService.Verify(m => m.GetAllSizes(), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetSizeById
        [Fact]
        public void GetSizeById_WhenCalled_ReturnsOkResult()
        {
            this._mockSizeService.Setup(s => s.GetSizeById(It.IsAny<int>()))
                .Returns(this.CreateMockSize(1, "Test"));

            var okResult = this._sizeController.GetSizeById(It.IsAny<int>()) as ObjectResult;

            this._mockSizeService.Verify(s => s.GetSizeById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetSizeById_WhenCalled_ReturnsNotFound()
        {
            this._mockSizeService.Setup(s => s.GetSizeById(It.IsAny<int>()))
                .Returns(null as Size);

            var notFoundResult = this._sizeController.GetSizeById(It.IsAny<int>()) as NotFoundResult;

            this._mockSizeService.Verify(s => s.GetSizeById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetSizeById_WhenCalled_ReturnsStatusCode500()
        {
            this._mockSizeService.Setup(s => s.GetSizeById(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._sizeController.GetSizeById(It.IsAny<int>()) as ObjectResult;

            this._mockSizeService.Verify(s => s.GetSizeById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region CreateSize
        [Fact]
        public void CreateSize_WhenCalled_WithSizeNull_ReturnsBadRequestResult()
        {
            this._mockSizeService.Setup(s => s.CreateSize(It.IsAny<Size>()));
            this._mockSizeService.Setup(s => s.Save());

            var badRequest = this._sizeController.CreateSize(null as Size) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateSize_WhenCalled_WithSizeWithIdNotNull_ReturnsBadRequestResult()
        {
            this._mockSizeService.Setup(s => s.CreateSize(It.IsAny<Size>()));
            this._mockSizeService.Setup(s => s.Save());

            var badRequest = this._sizeController.CreateSize(this.CreateMockSize(2)) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateSize_WhenCalled_ReturnsOkResult()
        {
            this._mockSizeService.Setup(s => s.CreateSize(It.IsAny<Size>()))
                .Callback<Size>(t => t.Id = 1);
            this._mockSizeService.Setup(s => s.Save());

            var okResult = this._sizeController.CreateSize(this.CreateMockSize(null, "Test")) as ObjectResult;

            this._mockSizeService.Verify(s => s.CreateSize(It.IsAny<Size>()), Times.Once);
            this._mockSizeService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [Fact]
        public void CreateSize_WhenCalled_ReturnsStatusCode500()
        {
            this._mockSizeService.Setup(s => s.CreateSize(It.IsAny<Size>()))
                .Throws(new Exception());
            this._mockSizeService.Setup(s => s.Save());

            var errorResult = this._sizeController.CreateSize(this.CreateMockSize(null, "Test")) as ObjectResult;

            this._mockSizeService.Verify(s => s.CreateSize(It.IsAny<Size>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region UpdateSize
        [Fact]
        public void UpdateSize_WhenCalled_WithSizeNull_ReturnsBadRequestResult()
        {
            this._mockSizeService.Setup(s => s.UpdateSize(It.IsAny<Size>(), It.IsAny<Size>()));
            this._mockSizeService.Setup(s => s.GetSizeById(It.IsAny<int>()))
                .Returns(It.IsAny<Size>());
            this._mockSizeService.Setup(s => s.Save());

            var badRequest = this._sizeController.UpdateSize(It.IsAny<int>(), null as Size) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateSize_WhenCalled_WithSizeWithIdNotFound_ReturnsBadRequestResult()
        {
            this._mockSizeService.Setup(s => s.UpdateSize(It.IsAny<Size>(), It.IsAny<Size>()));
            this._mockSizeService.Setup(s => s.GetSizeById(It.IsAny<int>()))
                .Returns(null as Size);
            this._mockSizeService.Setup(s => s.Save());

            var notFoundResult = this._sizeController.UpdateSize(It.IsAny<int>(), this.CreateMockSize(2)) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateSize_WhenCalled_ReturnsOkResult()
        {
            this._mockSizeService.Setup(s => s.UpdateSize(It.IsAny<Size>(), It.IsAny<Size>()));
            this._mockSizeService.Setup(s => s.GetSizeById(It.IsAny<int>()))
                .Returns(this.CreateMockSize(1));
            this._mockSizeService.Setup(s => s.Save());

            var okResult = this._sizeController.UpdateSize(It.IsAny<int>(), this.CreateMockSize(null, "Test")) as StatusCodeResult;

            this._mockSizeService.Verify(s => s.GetSizeById(It.IsAny<int>()), Times.Once);
            this._mockSizeService.Verify(s => s.UpdateSize(It.IsAny<Size>(), It.IsAny<Size>()), Times.Once);
            this._mockSizeService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void UpdateSize_WhenCalled_ReturnsStatusCode500()
        {
            this._mockSizeService.Setup(s => s.UpdateSize(It.IsAny<Size>(), It.IsAny<Size>()))
                .Throws(new Exception());
            this._mockSizeService.Setup(s => s.GetSizeById(It.IsAny<int>()))
                .Returns(this.CreateMockSize(1));
            this._mockSizeService.Setup(s => s.Save());

            var errorResult = this._sizeController.UpdateSize(It.IsAny<int>(), this.CreateMockSize(null, "Test")) as ObjectResult;

            this._mockSizeService.Verify(s => s.GetSizeById(It.IsAny<int>()), Times.Once);
            this._mockSizeService.Verify(s => s.UpdateSize(It.IsAny<Size>(), It.IsAny<Size>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region DeleteSize
        [Fact]
        public void DeleteSize_WhenCalled_WithSizeWithIdNotFound_ReturnsNotFound()
        {
            this._mockSizeService.Setup(s => s.DeleteSize(It.IsAny<Size>()));
            this._mockSizeService.Setup(s => s.GetSizeById(It.IsAny<int>()))
                .Returns(null as Size);
            this._mockSizeService.Setup(s => s.Save());

            var notFoundResult = this._sizeController.DeleteSize(It.IsAny<int>()) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void DeleteSize_WhenCalled_ReturnsOkResult()
        {
            this._mockSizeService.Setup(s => s.DeleteSize(It.IsAny<Size>()));
            this._mockSizeService.Setup(s => s.GetSizeById(It.IsAny<int>()))
                .Returns(this.CreateMockSize(1));
            this._mockSizeService.Setup(s => s.Save());

            var okResult = this._sizeController.DeleteSize(It.IsAny<int>()) as StatusCodeResult;

            this._mockSizeService.Verify(s => s.GetSizeById(It.IsAny<int>()), Times.Once);
            this._mockSizeService.Verify(s => s.DeleteSize(It.IsAny<Size>()), Times.Once);
            this._mockSizeService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void DeleteSize_WhenCalled_ReturnsStatusCode500()
        {
            this._mockSizeService.Setup(s => s.DeleteSize(It.IsAny<Size>()))
                .Throws(new Exception());
            this._mockSizeService.Setup(s => s.GetSizeById(It.IsAny<int>()))
                .Returns(this.CreateMockSize(1));
            this._mockSizeService.Setup(s => s.Save());

            var errorResult = this._sizeController.DeleteSize(It.IsAny<int>()) as ObjectResult;

            this._mockSizeService.Verify(s => s.GetSizeById(It.IsAny<int>()), Times.Once);
            this._mockSizeService.Verify(s => s.DeleteSize(It.IsAny<Size>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region Mock
        private Size CreateMockSize(int? id = null, string code = null)
        {
            return new Size()
            {
                Id = id,
                Code = code,
                Label = String.Format("{0} of {1}", "Label", code)
            };
        }
        #endregion
    }
}
