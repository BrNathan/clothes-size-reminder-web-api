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

        private Brand CreateMockBrand(int id, string name)
        {
            return new Brand()
            {
                Id = id,
                Name = name,
                CorporateName = name
            };
        }
    }
}
