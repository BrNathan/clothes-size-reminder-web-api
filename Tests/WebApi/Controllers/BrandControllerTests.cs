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
            var okResult = this._brandController.GetAllBrands() as OkObjectResult;
            this._mockBrandService.Verify(m => m.GetAllBrands(), Times.Once);

            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void GetAllBrands_WhenCalled_ReturnsStatusCode500()
        {
            this._mockBrandService.Setup(m => m.GetAllBrands()).Throws(new Exception());

            var errorResult = this._brandController.GetAllBrands() as ObjectResult;
            this._mockBrandService.Verify(m => m.GetAllBrands(), Times.Once);

            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
    }
}
