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
    public class ClothesSizeControllerTests
    {
        private readonly ClothesSizeController _clothesSizeController;
        private readonly Mock<IClothesSizeService> _mockClothesSizeService;

        public ClothesSizeControllerTests()
        {
            _mockClothesSizeService = new Mock<IClothesSizeService>();
            _clothesSizeController = new ClothesSizeController(_mockClothesSizeService.Object);
        }

        #region GetAllClothesSizes
        [Fact]
        public void GetAllClothesSizes_WhenCalled_ReturnsOkResult()
        {
            var okResult = this._clothesSizeController.GetAllClothesSizes() as ObjectResult;

            this._mockClothesSizeService.Verify(m => m.GetAllClothesSizes(), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetAllClothesSizes_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesSizeService.Setup(m => m.GetAllClothesSizes())
                .Throws(new Exception());

            var errorResult = this._clothesSizeController.GetAllClothesSizes() as ObjectResult;

            this._mockClothesSizeService.Verify(m => m.GetAllClothesSizes(), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetClothesSizeById
        [Fact]
        public void GetClothesSizeById_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesSizeService.Setup(s => s.GetClothesSizeById(It.IsAny<int>()))
                .Returns(this.CreateMockClothesSize(1));

            var okResult = this._clothesSizeController.GetClothesSizeById(It.IsAny<int>()) as ObjectResult;

            this._mockClothesSizeService.Verify(s => s.GetClothesSizeById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetClothesSizeById_WhenCalled_ReturnsNotFound()
        {
            this._mockClothesSizeService.Setup(s => s.GetClothesSizeById(It.IsAny<int>()))
                .Returns(null as ClothesSize);

            var notFoundResult = this._clothesSizeController.GetClothesSizeById(It.IsAny<int>()) as NotFoundResult;

            this._mockClothesSizeService.Verify(s => s.GetClothesSizeById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetClothesSizeById_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesSizeService.Setup(s => s.GetClothesSizeById(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._clothesSizeController.GetClothesSizeById(It.IsAny<int>()) as ObjectResult;

            this._mockClothesSizeService.Verify(s => s.GetClothesSizeById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region CreateClothesSize
        [Fact]
        public void CreateClothesSize_WhenCalled_WithClothesSizeNull_ReturnsBadRequestResult()
        {
            this._mockClothesSizeService.Setup(s => s.CreateClothesSize(It.IsAny<ClothesSize>()));
            this._mockClothesSizeService.Setup(s => s.Save());

            var badRequest = this._clothesSizeController.CreateClothesSize(null as ClothesSize) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateClothesSize_WhenCalled_WithClothesSizeWithIdNotNull_ReturnsBadRequestResult()
        {
            this._mockClothesSizeService.Setup(s => s.CreateClothesSize(It.IsAny<ClothesSize>()));
            this._mockClothesSizeService.Setup(s => s.Save());

            var badRequest = this._clothesSizeController.CreateClothesSize(this.CreateMockClothesSize(2)) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateClothesSize_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesSizeService.Setup(s => s.CreateClothesSize(It.IsAny<ClothesSize>()))
                .Callback<ClothesSize>(t => t.Id = 1);
            this._mockClothesSizeService.Setup(s => s.Save());

            var okResult = this._clothesSizeController.CreateClothesSize(this.CreateMockClothesSize(null)) as ObjectResult;

            this._mockClothesSizeService.Verify(s => s.CreateClothesSize(It.IsAny<ClothesSize>()), Times.Once);
            this._mockClothesSizeService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [Fact]
        public void CreateClothesSize_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesSizeService.Setup(s => s.CreateClothesSize(It.IsAny<ClothesSize>()))
                .Throws(new Exception());
            this._mockClothesSizeService.Setup(s => s.Save());

            var errorResult = this._clothesSizeController.CreateClothesSize(this.CreateMockClothesSize(null)) as ObjectResult;

            this._mockClothesSizeService.Verify(s => s.CreateClothesSize(It.IsAny<ClothesSize>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region UpdateClothesSize
        [Fact]
        public void UpdateClothesSize_WhenCalled_WithClothesSizeNull_ReturnsBadRequestResult()
        {
            this._mockClothesSizeService.Setup(s => s.UpdateClothesSize(It.IsAny<ClothesSize>(), It.IsAny<ClothesSize>()));
            this._mockClothesSizeService.Setup(s => s.GetClothesSizeById(It.IsAny<int>()))
                .Returns(It.IsAny<ClothesSize>());
            this._mockClothesSizeService.Setup(s => s.Save());

            var badRequest = this._clothesSizeController.UpdateClothesSize(It.IsAny<int>(), null as ClothesSize) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateClothesSize_WhenCalled_WithClothesSizeWithIdNotFound_ReturnsBadRequestResult()
        {
            this._mockClothesSizeService.Setup(s => s.UpdateClothesSize(It.IsAny<ClothesSize>(), It.IsAny<ClothesSize>()));
            this._mockClothesSizeService.Setup(s => s.GetClothesSizeById(It.IsAny<int>()))
                .Returns(null as ClothesSize);
            this._mockClothesSizeService.Setup(s => s.Save());

            var notFoundResult = this._clothesSizeController.UpdateClothesSize(It.IsAny<int>(), this.CreateMockClothesSize(2)) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateClothesSize_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesSizeService.Setup(s => s.UpdateClothesSize(It.IsAny<ClothesSize>(), It.IsAny<ClothesSize>()));
            this._mockClothesSizeService.Setup(s => s.GetClothesSizeById(It.IsAny<int>()))
                .Returns(this.CreateMockClothesSize(1));
            this._mockClothesSizeService.Setup(s => s.Save());

            var okResult = this._clothesSizeController.UpdateClothesSize(It.IsAny<int>(), this.CreateMockClothesSize(null)) as StatusCodeResult;

            this._mockClothesSizeService.Verify(s => s.GetClothesSizeById(It.IsAny<int>()), Times.Once);
            this._mockClothesSizeService.Verify(s => s.UpdateClothesSize(It.IsAny<ClothesSize>(), It.IsAny<ClothesSize>()), Times.Once);
            this._mockClothesSizeService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void UpdateClothesSize_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesSizeService.Setup(s => s.UpdateClothesSize(It.IsAny<ClothesSize>(), It.IsAny<ClothesSize>()))
                .Throws(new Exception());
            this._mockClothesSizeService.Setup(s => s.GetClothesSizeById(It.IsAny<int>()))
                .Returns(this.CreateMockClothesSize(1));
            this._mockClothesSizeService.Setup(s => s.Save());

            var errorResult = this._clothesSizeController.UpdateClothesSize(It.IsAny<int>(), this.CreateMockClothesSize(null)) as ObjectResult;

            this._mockClothesSizeService.Verify(s => s.GetClothesSizeById(It.IsAny<int>()), Times.Once);
            this._mockClothesSizeService.Verify(s => s.UpdateClothesSize(It.IsAny<ClothesSize>(), It.IsAny<ClothesSize>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region DeleteClothesSize
        [Fact]
        public void DeleteClothesSize_WhenCalled_WithClothesSizeWithIdNotFound_ReturnsNotFound()
        {
            this._mockClothesSizeService.Setup(s => s.DeleteClothesSize(It.IsAny<ClothesSize>()));
            this._mockClothesSizeService.Setup(s => s.GetClothesSizeById(It.IsAny<int>()))
                .Returns(null as ClothesSize);
            this._mockClothesSizeService.Setup(s => s.Save());

            var notFoundResult = this._clothesSizeController.DeleteClothesSize(It.IsAny<int>()) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void DeleteClothesSize_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesSizeService.Setup(s => s.DeleteClothesSize(It.IsAny<ClothesSize>()));
            this._mockClothesSizeService.Setup(s => s.GetClothesSizeById(It.IsAny<int>()))
                .Returns(this.CreateMockClothesSize(1));
            this._mockClothesSizeService.Setup(s => s.Save());

            var okResult = this._clothesSizeController.DeleteClothesSize(It.IsAny<int>()) as StatusCodeResult;

            this._mockClothesSizeService.Verify(s => s.GetClothesSizeById(It.IsAny<int>()), Times.Once);
            this._mockClothesSizeService.Verify(s => s.DeleteClothesSize(It.IsAny<ClothesSize>()), Times.Once);
            this._mockClothesSizeService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void DeleteClothesSize_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesSizeService.Setup(s => s.DeleteClothesSize(It.IsAny<ClothesSize>()))
                .Throws(new Exception());
            this._mockClothesSizeService.Setup(s => s.GetClothesSizeById(It.IsAny<int>()))
                .Returns(this.CreateMockClothesSize(1));
            this._mockClothesSizeService.Setup(s => s.Save());

            var errorResult = this._clothesSizeController.DeleteClothesSize(It.IsAny<int>()) as ObjectResult;

            this._mockClothesSizeService.Verify(s => s.GetClothesSizeById(It.IsAny<int>()), Times.Once);
            this._mockClothesSizeService.Verify(s => s.DeleteClothesSize(It.IsAny<ClothesSize>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region Mock
        private ClothesSize CreateMockClothesSize(int? id = null, int sizeId = 2, int clotheId = 3)
        {
            return new ClothesSize()
            {
                Id = id,
                SizeId = sizeId,
                ClothesId= clotheId
            };
        }
        #endregion
    }
}
