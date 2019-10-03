using BLL.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;
using Xunit;

namespace Tests.WebApi.Controllers
{
    public class BrandControllerTests
    {
        private readonly BrandController _brandController;
        private readonly Mock<IBrandService> _mockBrandService;

        public BrandControllerTests()
        {
            _mockBrandService = new Mock<IBrandService>();
            _brandController = new BrandController(_mockBrandService.Object);
        }

        #region GetAllBrands
        [Fact]
        public void GetAllBrands_WhenCalled_ReturnsOkResult()
        {
            var okResult = this._brandController.GetAllBrands() as ObjectResult;

            this._mockBrandService.Verify(m => m.GetAllBrands(), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetAllBrands_WhenCalled_ReturnsStatusCode500()
        {
            this._mockBrandService.Setup(m => m.GetAllBrands())
                .Throws(new Exception());

            var errorResult = this._brandController.GetAllBrands() as ObjectResult;

            this._mockBrandService.Verify(m => m.GetAllBrands(), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetBrandById
        [Fact]
        public void GetBrandById_WhenCalled_ReturnsOkResult()
        {
            this._mockBrandService.Setup(s => s.GetBrandById(It.IsAny<int>()))
                .Returns(this.CreateMockBrand(1, "Test"));

            var okResult = this._brandController.GetBrandById(It.IsAny<int>()) as ObjectResult;

            this._mockBrandService.Verify(s => s.GetBrandById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetBrandById_WhenCalled_ReturnsNotFound()
        {
            this._mockBrandService.Setup(s => s.GetBrandById(It.IsAny<int>()))
                .Returns(null as Brand);

            var notFoundResult = this._brandController.GetBrandById(It.IsAny<int>()) as NotFoundResult;

            this._mockBrandService.Verify(s => s.GetBrandById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetBrandById_WhenCalled_ReturnsStatusCode500()
        {
            this._mockBrandService.Setup(s => s.GetBrandById(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._brandController.GetBrandById(It.IsAny<int>()) as ObjectResult;

            this._mockBrandService.Verify(s => s.GetBrandById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region CreateBrand
        [Fact]
        public void CreateBrand_WhenCalled_WithBrandNull_ReturnsBadRequestResult()
        {
            this._mockBrandService.Setup(s => s.CreateBrand(It.IsAny<Brand>()));
            this._mockBrandService.Setup(s => s.Save());

            var badRequest = this._brandController.CreateBrand(null as Brand) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateBrand_WhenCalled_WithBrandWithIdNotNull_ReturnsBadRequestResult()
        {
            this._mockBrandService.Setup(s => s.CreateBrand(It.IsAny<Brand>()));
            this._mockBrandService.Setup(s => s.Save());

            var badRequest = this._brandController.CreateBrand(this.CreateMockBrand(2)) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateBrand_WhenCalled_ReturnsOkResult()
        {
            this._mockBrandService.Setup(s => s.CreateBrand(It.IsAny<Brand>()))
                .Callback<Brand>(t => t.Id = 1);
            this._mockBrandService.Setup(s => s.Save());

            var okResult = this._brandController.CreateBrand(this.CreateMockBrand(null, "Test")) as ObjectResult;

            this._mockBrandService.Verify(s => s.CreateBrand(It.IsAny<Brand>()), Times.Once);
            this._mockBrandService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [Fact]
        public void CreateBrand_WhenCalled_ReturnsStatusCode500()
        {
            this._mockBrandService.Setup(s => s.CreateBrand(It.IsAny<Brand>()))
                .Throws(new Exception());
            this._mockBrandService.Setup(s => s.Save());

            var errorResult = this._brandController.CreateBrand(this.CreateMockBrand(null, "Test")) as ObjectResult;

            this._mockBrandService.Verify(s => s.CreateBrand(It.IsAny<Brand>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region UpdateBrand
        [Fact]
        public void UpdateBrand_WhenCalled_WithBrandNull_ReturnsBadRequestResult()
        {
            this._mockBrandService.Setup(s => s.UpdateBrand(It.IsAny<Brand>(), It.IsAny<Brand>()));
            this._mockBrandService.Setup(s => s.GetBrandById(It.IsAny<int>()))
                .Returns(It.IsAny<Brand>());
            this._mockBrandService.Setup(s => s.Save());

            var badRequest = this._brandController.UpdateBrand(It.IsAny<int>(), null as Brand) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateBrand_WhenCalled_WithBrandWithIdNotFound_ReturnsBadRequestResult()
        {
            this._mockBrandService.Setup(s => s.UpdateBrand(It.IsAny<Brand>(), It.IsAny<Brand>()));
            this._mockBrandService.Setup(s => s.GetBrandById(It.IsAny<int>()))
                .Returns(null as Brand);
            this._mockBrandService.Setup(s => s.Save());

            var notFoundResult = this._brandController.UpdateBrand(It.IsAny<int>(), this.CreateMockBrand(2)) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateBrand_WhenCalled_ReturnsOkResult()
        {
            this._mockBrandService.Setup(s => s.UpdateBrand(It.IsAny<Brand>(), It.IsAny<Brand>()));
            this._mockBrandService.Setup(s => s.GetBrandById(It.IsAny<int>()))
                .Returns(this.CreateMockBrand(1));
            this._mockBrandService.Setup(s => s.Save());

            var okResult = this._brandController.UpdateBrand(It.IsAny<int>(), this.CreateMockBrand(null, "Test")) as StatusCodeResult;

            this._mockBrandService.Verify(s => s.GetBrandById(It.IsAny<int>()), Times.Once);
            this._mockBrandService.Verify(s => s.UpdateBrand(It.IsAny<Brand>(), It.IsAny<Brand>()), Times.Once);
            this._mockBrandService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void UpdateBrand_WhenCalled_ReturnsStatusCode500()
        {
            this._mockBrandService.Setup(s => s.UpdateBrand(It.IsAny<Brand>(), It.IsAny<Brand>()))
                .Throws(new Exception());
            this._mockBrandService.Setup(s => s.GetBrandById(It.IsAny<int>()))
                .Returns(this.CreateMockBrand(1));
            this._mockBrandService.Setup(s => s.Save());

            var errorResult = this._brandController.UpdateBrand(It.IsAny<int>(), this.CreateMockBrand(null, "Test")) as ObjectResult;

            this._mockBrandService.Verify(s => s.GetBrandById(It.IsAny<int>()), Times.Once);
            this._mockBrandService.Verify(s => s.UpdateBrand(It.IsAny<Brand>(), It.IsAny<Brand>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region DeleteBrand
        [Fact]
        public void DeleteBrand_WhenCalled_WithBrandWithIdNotFound_ReturnsNotFound()
        {
            this._mockBrandService.Setup(s => s.DeleteBrand(It.IsAny<Brand>()));
            this._mockBrandService.Setup(s => s.GetBrandById(It.IsAny<int>()))
                .Returns(null as Brand);
            this._mockBrandService.Setup(s => s.Save());

            var notFoundResult = this._brandController.DeleteBrand(It.IsAny<int>()) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void DeleteBrand_WhenCalled_ReturnsOkResult()
        {
            this._mockBrandService.Setup(s => s.DeleteBrand(It.IsAny<Brand>()));
            this._mockBrandService.Setup(s => s.GetBrandById(It.IsAny<int>()))
                .Returns(this.CreateMockBrand(1));
            this._mockBrandService.Setup(s => s.Save());

            var okResult = this._brandController.DeleteBrand(It.IsAny<int>()) as StatusCodeResult;

            this._mockBrandService.Verify(s => s.GetBrandById(It.IsAny<int>()), Times.Once);
            this._mockBrandService.Verify(s => s.DeleteBrand(It.IsAny<Brand>()), Times.Once);
            this._mockBrandService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void DeleteBrand_WhenCalled_ReturnsStatusCode500()
        {
            this._mockBrandService.Setup(s => s.DeleteBrand(It.IsAny<Brand>()))
                .Throws(new Exception());
            this._mockBrandService.Setup(s => s.GetBrandById(It.IsAny<int>()))
                .Returns(this.CreateMockBrand(1));
            this._mockBrandService.Setup(s => s.Save());

            var errorResult = this._brandController.DeleteBrand(It.IsAny<int>()) as ObjectResult;

            this._mockBrandService.Verify(s => s.GetBrandById(It.IsAny<int>()), Times.Once);
            this._mockBrandService.Verify(s => s.DeleteBrand(It.IsAny<Brand>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region Mock
        private Brand CreateMockBrand(int? id = null, string name = null)
        {
            return new Brand()
            {
                Id = id,
                Name = name,
                CorporateName = name
            };
        }
        #endregion
    }
}
