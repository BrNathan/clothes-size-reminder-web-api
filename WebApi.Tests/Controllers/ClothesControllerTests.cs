using BLL.Interfaces;
using Entities.ExtendedModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using WebApi.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class ClothesControllerTests
    {
        private readonly ClothesController _clothesController;
        private readonly Mock<IClothesService> _mockClothesService;

        public ClothesControllerTests()
        {
            _mockClothesService = new Mock<IClothesService>();
            _clothesController = new ClothesController(_mockClothesService.Object);
        }

        #region GetAllClothes
        [Fact]
        public void GetAllClothes_WhenCalled_ReturnsOkResult()
        {
            var okResult = this._clothesController.GetAllClothes() as ObjectResult;

            this._mockClothesService.Verify(m => m.GetAllClothes(), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetAllClothes_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesService.Setup(m => m.GetAllClothes())
                .Throws(new Exception());

            var errorResult = this._clothesController.GetAllClothes() as ObjectResult;

            this._mockClothesService.Verify(m => m.GetAllClothes(), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetClothesById
        [Fact]
        public void GetClothesById_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesService.Setup(s => s.GetClothesById(It.IsAny<int>()))
                .Returns(this.CreateMockClothes(1, "Test"));

            var okResult = this._clothesController.GetClothesById(It.IsAny<int>()) as ObjectResult;

            this._mockClothesService.Verify(s => s.GetClothesById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetClothesById_WhenCalled_ReturnsNotFound()
        {
            this._mockClothesService.Setup(s => s.GetClothesById(It.IsAny<int>()))
                .Returns(null as Clothes);

            var notFoundResult = this._clothesController.GetClothesById(It.IsAny<int>()) as NotFoundResult;

            this._mockClothesService.Verify(s => s.GetClothesById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetClothesById_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesService.Setup(s => s.GetClothesById(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._clothesController.GetClothesById(It.IsAny<int>()) as ObjectResult;

            this._mockClothesService.Verify(s => s.GetClothesById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetClothesWithDetails
        [Fact]
        public void GetClothesWithDetails_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesService.Setup(s => s.GetClothesWithDetails(It.IsAny<int>()))
                .Returns(this.CreateMockClothesExtended(1, "Test"));

            var okResult = this._clothesController.GetClothesWithDetails(It.IsAny<int>()) as ObjectResult;

            this._mockClothesService.Verify(s => s.GetClothesWithDetails(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetClothesWithDetails_WhenCalled_ReturnsNotFound()
        {
            this._mockClothesService.Setup(s => s.GetClothesWithDetails(It.IsAny<int>()))
                .Returns(null as ClothesExtended);

            var notFoundResult = this._clothesController.GetClothesWithDetails(It.IsAny<int>()) as NotFoundResult;

            this._mockClothesService.Verify(s => s.GetClothesWithDetails(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetClothesWithDetails_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesService.Setup(s => s.GetClothesWithDetails(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._clothesController.GetClothesWithDetails(It.IsAny<int>()) as ObjectResult;

            this._mockClothesService.Verify(s => s.GetClothesWithDetails(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region CreateClothes
        [Fact]
        public void CreateClothes_WhenCalled_WithClothesNull_ReturnsBadRequestResult()
        {
            this._mockClothesService.Setup(s => s.CreateClothes(It.IsAny<Clothes>()));
            this._mockClothesService.Setup(s => s.Save());

            var badRequest = this._clothesController.CreateClothes(null as Clothes) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateClothes_WhenCalled_WithClothesWithIdNotNull_ReturnsBadRequestResult()
        {
            this._mockClothesService.Setup(s => s.CreateClothes(It.IsAny<Clothes>()));
            this._mockClothesService.Setup(s => s.Save());

            var badRequest = this._clothesController.CreateClothes(this.CreateMockClothes(2)) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateClothes_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesService.Setup(s => s.CreateClothes(It.IsAny<Clothes>()))
                .Callback<Clothes>(t => t.Id = 1);
            this._mockClothesService.Setup(s => s.Save());

            var okResult = this._clothesController.CreateClothes(this.CreateMockClothes(null, "Test")) as ObjectResult;

            this._mockClothesService.Verify(s => s.CreateClothes(It.IsAny<Clothes>()), Times.Once);
            this._mockClothesService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [Fact]
        public void CreateClothes_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesService.Setup(s => s.CreateClothes(It.IsAny<Clothes>()))
                .Throws(new Exception());
            this._mockClothesService.Setup(s => s.Save());

            var errorResult = this._clothesController.CreateClothes(this.CreateMockClothes(null, "Test")) as ObjectResult;

            this._mockClothesService.Verify(s => s.CreateClothes(It.IsAny<Clothes>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region UpdateClothes
        [Fact]
        public void UpdateClothes_WhenCalled_WithClothesNull_ReturnsBadRequestResult()
        {
            this._mockClothesService.Setup(s => s.UpdateClothes(It.IsAny<Clothes>(), It.IsAny<Clothes>()));
            this._mockClothesService.Setup(s => s.GetClothesById(It.IsAny<int>()))
                .Returns(It.IsAny<Clothes>());
            this._mockClothesService.Setup(s => s.Save());

            var badRequest = this._clothesController.UpdateClothes(It.IsAny<int>(), null as Clothes) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateClothes_WhenCalled_WithClothesWithIdNotFound_ReturnsBadRequestResult()
        {
            this._mockClothesService.Setup(s => s.UpdateClothes(It.IsAny<Clothes>(), It.IsAny<Clothes>()));
            this._mockClothesService.Setup(s => s.GetClothesById(It.IsAny<int>()))
                .Returns(null as Clothes);
            this._mockClothesService.Setup(s => s.Save());

            var notFoundResult = this._clothesController.UpdateClothes(It.IsAny<int>(), this.CreateMockClothes(2)) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateClothes_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesService.Setup(s => s.UpdateClothes(It.IsAny<Clothes>(), It.IsAny<Clothes>()));
            this._mockClothesService.Setup(s => s.GetClothesById(It.IsAny<int>()))
                .Returns(this.CreateMockClothes(1));
            this._mockClothesService.Setup(s => s.Save());

            var okResult = this._clothesController.UpdateClothes(It.IsAny<int>(), this.CreateMockClothes(null, "Test")) as StatusCodeResult;

            this._mockClothesService.Verify(s => s.GetClothesById(It.IsAny<int>()), Times.Once);
            this._mockClothesService.Verify(s => s.UpdateClothes(It.IsAny<Clothes>(), It.IsAny<Clothes>()), Times.Once);
            this._mockClothesService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void UpdateClothes_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesService.Setup(s => s.UpdateClothes(It.IsAny<Clothes>(), It.IsAny<Clothes>()))
                .Throws(new Exception());
            this._mockClothesService.Setup(s => s.GetClothesById(It.IsAny<int>()))
                .Returns(this.CreateMockClothes(1));
            this._mockClothesService.Setup(s => s.Save());

            var errorResult = this._clothesController.UpdateClothes(It.IsAny<int>(), this.CreateMockClothes(null, "Test")) as ObjectResult;

            this._mockClothesService.Verify(s => s.GetClothesById(It.IsAny<int>()), Times.Once);
            this._mockClothesService.Verify(s => s.UpdateClothes(It.IsAny<Clothes>(), It.IsAny<Clothes>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region DeleteClothes
        [Fact]
        public void DeleteClothes_WhenCalled_WithClothesWithIdNotFound_ReturnsNotFound()
        {
            this._mockClothesService.Setup(s => s.DeleteClothes(It.IsAny<Clothes>()));
            this._mockClothesService.Setup(s => s.GetClothesById(It.IsAny<int>()))
                .Returns(null as Clothes);
            this._mockClothesService.Setup(s => s.Save());

            var notFoundResult = this._clothesController.DeleteClothes(It.IsAny<int>()) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void DeleteClothes_WhenCalled_ReturnsOkResult()
        {
            this._mockClothesService.Setup(s => s.DeleteClothes(It.IsAny<Clothes>()));
            this._mockClothesService.Setup(s => s.GetClothesById(It.IsAny<int>()))
                .Returns(this.CreateMockClothes(1));
            this._mockClothesService.Setup(s => s.Save());

            var okResult = this._clothesController.DeleteClothes(It.IsAny<int>()) as StatusCodeResult;

            this._mockClothesService.Verify(s => s.GetClothesById(It.IsAny<int>()), Times.Once);
            this._mockClothesService.Verify(s => s.DeleteClothes(It.IsAny<Clothes>()), Times.Once);
            this._mockClothesService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void DeleteClothes_WhenCalled_ReturnsStatusCode500()
        {
            this._mockClothesService.Setup(s => s.DeleteClothes(It.IsAny<Clothes>()))
                .Throws(new Exception());
            this._mockClothesService.Setup(s => s.GetClothesById(It.IsAny<int>()))
                .Returns(this.CreateMockClothes(1));
            this._mockClothesService.Setup(s => s.Save());

            var errorResult = this._clothesController.DeleteClothes(It.IsAny<int>()) as ObjectResult;

            this._mockClothesService.Verify(s => s.GetClothesById(It.IsAny<int>()), Times.Once);
            this._mockClothesService.Verify(s => s.DeleteClothes(It.IsAny<Clothes>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region Mock
        private Clothes CreateMockClothes(int? id = null, string code = null, int genderId = 1, int clothesCategoryId = 2)
        {
            return new Clothes()
            {
                Id = id,
                Code = code,
                Label = String.Format("{0} {1}", "Label", code),
                GenderId = genderId,
                ClothesCategoryId = clothesCategoryId
            };
        }

        private ClothesExtended CreateMockClothesExtended(int id = 0, string code = null, int genderId = 1, int clothesCategoryId = 2)
        {
            return new ClothesExtended()
            {
                Id = id,
                Code = code,
                Label = String.Format("{0} {1}", "Label", code),
                Gender = new Gender()
                {
                    Id = 1,
                    Code = "Code Gender",
                    Label = "Label Gender"
                },
                ClothesCategory = new ClothesCategory()
                {
                    Id = 2,
                    Label = "Label Category",
                    Code = "Code Category"
                }
            };
        }
        #endregion
    }
}
