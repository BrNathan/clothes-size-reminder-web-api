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
    public class ClothesCategoryControllerTests
    {
        private readonly ClothesCategoryController _clothesCategoryController;
        private readonly Mock<IClothesCategoryService> _mockClothesCategoryService;

        public ClothesCategoryControllerTests()
        {
            _mockClothesCategoryService = new Mock<IClothesCategoryService>();
            _clothesCategoryController = new ClothesCategoryController(_mockClothesCategoryService.Object);
        }

        #region GetAllClothesCategories
        [Fact]
        public void GetAllClothesCategories_WhenCalled_ReturnsOkResult()
        {
            var okResult = this._clothesCategoryController.GetAllClothesCategories() as ObjectResult;

            this._mockClothesCategoryService.Verify(m => m.GetAllClothesCategories(), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetAllClothesCategories_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesCategoryService.Setup(m => m.GetAllClothesCategories())
                .Throws(new Exception());

            var errorResult = this._clothesCategoryController.GetAllClothesCategories() as ObjectResult;

            this._mockClothesCategoryService.Verify(m => m.GetAllClothesCategories(), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetClothesCategoryById
        [Fact]
        public void GetClothesCategoryById_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesCategoryService.Setup(s => s.GetClothesCategoryById(It.IsAny<int>()))
                .Returns(this.CreateMockClothesCategory(1, "Test"));

            var okResult = this._clothesCategoryController.GetClothesCategoryById(It.IsAny<int>()) as ObjectResult;

            this._mockClothesCategoryService.Verify(s => s.GetClothesCategoryById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetClothesCategoryById_WhenCalled_ReturnsNotFound()
        {
            this._mockClothesCategoryService.Setup(s => s.GetClothesCategoryById(It.IsAny<int>()))
                .Returns(null as ClothesCategory);

            var notFoundResult = this._clothesCategoryController.GetClothesCategoryById(It.IsAny<int>()) as NotFoundResult;

            this._mockClothesCategoryService.Verify(s => s.GetClothesCategoryById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetClothesCategoryById_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesCategoryService.Setup(s => s.GetClothesCategoryById(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._clothesCategoryController.GetClothesCategoryById(It.IsAny<int>()) as ObjectResult;

            this._mockClothesCategoryService.Verify(s => s.GetClothesCategoryById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region CreateClothesCategory
        [Fact]
        public void CreateClothesCategory_WhenCalled_WithClothesCategoryNull_ReturnsBadRequestResult()
        {
            this._mockClothesCategoryService.Setup(s => s.CreateClothesCategory(It.IsAny<ClothesCategory>()));
            this._mockClothesCategoryService.Setup(s => s.Save());

            var badRequest = this._clothesCategoryController.CreateClothesCategory(null as ClothesCategory) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateClothesCategory_WhenCalled_WithClothesCategoryWithIdNotNull_ReturnsBadRequestResult()
        {
            this._mockClothesCategoryService.Setup(s => s.CreateClothesCategory(It.IsAny<ClothesCategory>()));
            this._mockClothesCategoryService.Setup(s => s.Save());

            var badRequest = this._clothesCategoryController.CreateClothesCategory(this.CreateMockClothesCategory(2)) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateClothesCategory_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesCategoryService.Setup(s => s.CreateClothesCategory(It.IsAny<ClothesCategory>()))
                .Callback<ClothesCategory>(t => t.Id = 1);
            this._mockClothesCategoryService.Setup(s => s.Save());

            var okResult = this._clothesCategoryController.CreateClothesCategory(this.CreateMockClothesCategory(null, "Test")) as ObjectResult;

            this._mockClothesCategoryService.Verify(s => s.CreateClothesCategory(It.IsAny<ClothesCategory>()), Times.Once);
            this._mockClothesCategoryService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [Fact]
        public void CreateClothesCategory_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesCategoryService.Setup(s => s.CreateClothesCategory(It.IsAny<ClothesCategory>()))
                .Throws(new Exception());
            this._mockClothesCategoryService.Setup(s => s.Save());

            var errorResult = this._clothesCategoryController.CreateClothesCategory(this.CreateMockClothesCategory(null, "Test")) as ObjectResult;

            this._mockClothesCategoryService.Verify(s => s.CreateClothesCategory(It.IsAny<ClothesCategory>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region UpdateClothesCategory
        [Fact]
        public void UpdateClothesCategory_WhenCalled_WithClothesCategoryNull_ReturnsBadRequestResult()
        {
            this._mockClothesCategoryService.Setup(s => s.UpdateClothesCategory(It.IsAny<ClothesCategory>(), It.IsAny<ClothesCategory>()));
            this._mockClothesCategoryService.Setup(s => s.GetClothesCategoryById(It.IsAny<int>()))
                .Returns(It.IsAny<ClothesCategory>());
            this._mockClothesCategoryService.Setup(s => s.Save());

            var badRequest = this._clothesCategoryController.UpdateClothesCategory(It.IsAny<int>(), null as ClothesCategory) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateClothesCategory_WhenCalled_WithClothesCategoryWithIdNotFound_ReturnsBadRequestResult()
        {
            this._mockClothesCategoryService.Setup(s => s.UpdateClothesCategory(It.IsAny<ClothesCategory>(), It.IsAny<ClothesCategory>()));
            this._mockClothesCategoryService.Setup(s => s.GetClothesCategoryById(It.IsAny<int>()))
                .Returns(null as ClothesCategory);
            this._mockClothesCategoryService.Setup(s => s.Save());

            var notFoundResult = this._clothesCategoryController.UpdateClothesCategory(It.IsAny<int>(), this.CreateMockClothesCategory(2)) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateClothesCategory_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesCategoryService.Setup(s => s.UpdateClothesCategory(It.IsAny<ClothesCategory>(), It.IsAny<ClothesCategory>()));
            this._mockClothesCategoryService.Setup(s => s.GetClothesCategoryById(It.IsAny<int>()))
                .Returns(this.CreateMockClothesCategory(1));
            this._mockClothesCategoryService.Setup(s => s.Save());

            var okResult = this._clothesCategoryController.UpdateClothesCategory(It.IsAny<int>(), this.CreateMockClothesCategory(null, "Test")) as StatusCodeResult;

            this._mockClothesCategoryService.Verify(s => s.GetClothesCategoryById(It.IsAny<int>()), Times.Once);
            this._mockClothesCategoryService.Verify(s => s.UpdateClothesCategory(It.IsAny<ClothesCategory>(), It.IsAny<ClothesCategory>()), Times.Once);
            this._mockClothesCategoryService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void UpdateClothesCategory_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesCategoryService.Setup(s => s.UpdateClothesCategory(It.IsAny<ClothesCategory>(), It.IsAny<ClothesCategory>()))
                .Throws(new Exception());
            this._mockClothesCategoryService.Setup(s => s.GetClothesCategoryById(It.IsAny<int>()))
                .Returns(this.CreateMockClothesCategory(1));
            this._mockClothesCategoryService.Setup(s => s.Save());

            var errorResult = this._clothesCategoryController.UpdateClothesCategory(It.IsAny<int>(), this.CreateMockClothesCategory(null, "Test")) as ObjectResult;

            this._mockClothesCategoryService.Verify(s => s.GetClothesCategoryById(It.IsAny<int>()), Times.Once);
            this._mockClothesCategoryService.Verify(s => s.UpdateClothesCategory(It.IsAny<ClothesCategory>(), It.IsAny<ClothesCategory>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region DeleteClothesCategory
        [Fact]
        public void DeleteClothesCategory_WhenCalled_WithClothesCategoryWithIdNotFound_ReturnsNotFound()
        {
            this._mockClothesCategoryService.Setup(s => s.DeleteClothesCategory(It.IsAny<ClothesCategory>()));
            this._mockClothesCategoryService.Setup(s => s.GetClothesCategoryById(It.IsAny<int>()))
                .Returns(null as ClothesCategory);
            this._mockClothesCategoryService.Setup(s => s.Save());

            var notFoundResult = this._clothesCategoryController.DeleteClothesCategory(It.IsAny<int>()) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void DeleteClothesCategory_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesCategoryService.Setup(s => s.DeleteClothesCategory(It.IsAny<ClothesCategory>()));
            this._mockClothesCategoryService.Setup(s => s.GetClothesCategoryById(It.IsAny<int>()))
                .Returns(this.CreateMockClothesCategory(1));
            this._mockClothesCategoryService.Setup(s => s.Save());

            var okResult = this._clothesCategoryController.DeleteClothesCategory(It.IsAny<int>()) as StatusCodeResult;

            this._mockClothesCategoryService.Verify(s => s.GetClothesCategoryById(It.IsAny<int>()), Times.Once);
            this._mockClothesCategoryService.Verify(s => s.DeleteClothesCategory(It.IsAny<ClothesCategory>()), Times.Once);
            this._mockClothesCategoryService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void DeleteClothesCategory_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesCategoryService.Setup(s => s.DeleteClothesCategory(It.IsAny<ClothesCategory>()))
                .Throws(new Exception());
            this._mockClothesCategoryService.Setup(s => s.GetClothesCategoryById(It.IsAny<int>()))
                .Returns(this.CreateMockClothesCategory(1));
            this._mockClothesCategoryService.Setup(s => s.Save());

            var errorResult = this._clothesCategoryController.DeleteClothesCategory(It.IsAny<int>()) as ObjectResult;

            this._mockClothesCategoryService.Verify(s => s.GetClothesCategoryById(It.IsAny<int>()), Times.Once);
            this._mockClothesCategoryService.Verify(s => s.DeleteClothesCategory(It.IsAny<ClothesCategory>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region Mock
        private ClothesCategory CreateMockClothesCategory(int? id = null, string code = null)
        {
            return new ClothesCategory()
            {
                Id = id,
                Code = code,
                Label = code
            };
        }
        #endregion
    }
}
