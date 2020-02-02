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
    public class ReminderControllerTests
    {
        private readonly ReminderController _reminderController;
        private readonly Mock<IReminderService> _mockReminderService;
        private readonly Mock<IClothesSizeService> _mockClothesSizeService;

        public ReminderControllerTests()
        {
            _mockReminderService = new Mock<IReminderService>();
            _mockClothesSizeService = new Mock<IClothesSizeService>();
            _reminderController = new ReminderController(_mockReminderService.Object, _mockClothesSizeService.Object);
        }

        #region GetAllReminders
        [Fact]
        public void GetAllReminders_WhenCalled_ReturnsOkResult()
        {
            var okResult = this._reminderController.GetAllReminders() as ObjectResult;

            this._mockReminderService.Verify(m => m.GetAllReminders(), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetAllReminders_WhenCalled_ReturnsStatusCode500()
        {
            this._mockReminderService.Setup(m => m.GetAllReminders())
                .Throws(new Exception());

            var errorResult = this._reminderController.GetAllReminders() as ObjectResult;

            this._mockReminderService.Verify(m => m.GetAllReminders(), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region GetReminderById
        [Fact]
        public void GetReminderById_WhenCalled_ReturnsOkResult()
        {
            this._mockReminderService.Setup(s => s.GetReminderById(It.IsAny<int>()))
                .Returns(this.CreateMockReminder(1));

            var okResult = this._reminderController.GetReminderById(It.IsAny<int>()) as ObjectResult;

            this._mockReminderService.Verify(s => s.GetReminderById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetReminderById_WhenCalled_ReturnsNotFound()
        {
            this._mockReminderService.Setup(s => s.GetReminderById(It.IsAny<int>()))
                .Returns(null as Reminder);

            var notFoundResult = this._reminderController.GetReminderById(It.IsAny<int>()) as NotFoundResult;

            this._mockReminderService.Verify(s => s.GetReminderById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetReminderById_WhenCalled_ReturnsStatusCode500()
        {
            this._mockReminderService.Setup(s => s.GetReminderById(It.IsAny<int>()))
                .Throws(new Exception());

            var errorResult = this._reminderController.GetReminderById(It.IsAny<int>()) as ObjectResult;

            this._mockReminderService.Verify(s => s.GetReminderById(It.IsAny<int>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region CreateReminder
        [Fact]
        public void CreateReminder_WhenCalled_WithReminderNull_ReturnsBadRequestResult()
        {
            this._mockReminderService.Setup(s => s.CreateReminder(It.IsAny<Reminder>()));
            this._mockReminderService.Setup(s => s.Save());

            var badRequest = this._reminderController.CreateReminder(null as Reminder) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateReminder_WhenCalled_WithReminderWithIdNotNull_ReturnsBadRequestResult()
        {
            this._mockReminderService.Setup(s => s.CreateReminder(It.IsAny<Reminder>()));
            this._mockReminderService.Setup(s => s.Save());

            var badRequest = this._reminderController.CreateReminder(this.CreateMockReminder(2)) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void CreateReminder_WhenCalled_ReturnsOkResult()
        {
            this._mockReminderService.Setup(s => s.CreateReminder(It.IsAny<Reminder>()))
                .Callback<Reminder>(t => t.Id = 1);
            this._mockReminderService.Setup(s => s.Save());

            var okResult = this._reminderController.CreateReminder(this.CreateMockReminder(null)) as ObjectResult;

            this._mockReminderService.Verify(s => s.CreateReminder(It.IsAny<Reminder>()), Times.Once);
            this._mockReminderService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }

        [Fact]
        public void CreateReminder_WhenCalled_ReturnsStatusCode500()
        {
            this._mockReminderService.Setup(s => s.CreateReminder(It.IsAny<Reminder>()))
                .Throws(new Exception());
            this._mockReminderService.Setup(s => s.Save());

            var errorResult = this._reminderController.CreateReminder(this.CreateMockReminder(null)) as ObjectResult;

            this._mockReminderService.Verify(s => s.CreateReminder(It.IsAny<Reminder>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region UpdateReminder
        [Fact]
        public void UpdateReminder_WhenCalled_WithReminderNull_ReturnsBadRequestResult()
        {
            this._mockReminderService.Setup(s => s.UpdateReminder(It.IsAny<Reminder>(), It.IsAny<Reminder>()));
            this._mockReminderService.Setup(s => s.GetReminderById(It.IsAny<int>()))
                .Returns(It.IsAny<Reminder>());
            this._mockReminderService.Setup(s => s.Save());

            var badRequest = this._reminderController.UpdateReminder(It.IsAny<int>(), null as Reminder) as ObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateReminder_WhenCalled_WithReminderWithIdNotFound_ReturnsBadRequestResult()
        {
            this._mockReminderService.Setup(s => s.UpdateReminder(It.IsAny<Reminder>(), It.IsAny<Reminder>()));
            this._mockReminderService.Setup(s => s.GetReminderById(It.IsAny<int>()))
                .Returns(null as Reminder);
            this._mockReminderService.Setup(s => s.Save());

            var notFoundResult = this._reminderController.UpdateReminder(It.IsAny<int>(), this.CreateMockReminder(2)) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void UpdateReminder_WhenCalled_ReturnsOkResult()
        {
            this._mockReminderService.Setup(s => s.UpdateReminder(It.IsAny<Reminder>(), It.IsAny<Reminder>()));
            this._mockReminderService.Setup(s => s.GetReminderById(It.IsAny<int>()))
                .Returns(this.CreateMockReminder(1));
            this._mockReminderService.Setup(s => s.Save());

            var okResult = this._reminderController.UpdateReminder(It.IsAny<int>(), this.CreateMockReminder(null)) as StatusCodeResult;

            this._mockReminderService.Verify(s => s.GetReminderById(It.IsAny<int>()), Times.Once);
            this._mockReminderService.Verify(s => s.UpdateReminder(It.IsAny<Reminder>(), It.IsAny<Reminder>()), Times.Once);
            this._mockReminderService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void UpdateReminder_WhenCalled_ReturnsStatusCode500()
        {
            this._mockReminderService.Setup(s => s.UpdateReminder(It.IsAny<Reminder>(), It.IsAny<Reminder>()))
                .Throws(new Exception());
            this._mockReminderService.Setup(s => s.GetReminderById(It.IsAny<int>()))
                .Returns(this.CreateMockReminder(1));
            this._mockReminderService.Setup(s => s.Save());

            var errorResult = this._reminderController.UpdateReminder(It.IsAny<int>(), this.CreateMockReminder(null)) as ObjectResult;

            this._mockReminderService.Verify(s => s.GetReminderById(It.IsAny<int>()), Times.Once);
            this._mockReminderService.Verify(s => s.UpdateReminder(It.IsAny<Reminder>(), It.IsAny<Reminder>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region DeleteReminder
        [Fact]
        public void DeleteReminder_WhenCalled_WithReminderWithIdNotFound_ReturnsNotFound()
        {
            this._mockReminderService.Setup(s => s.DeleteReminder(It.IsAny<Reminder>()));
            this._mockReminderService.Setup(s => s.GetReminderById(It.IsAny<int>()))
                .Returns(null as Reminder);
            this._mockReminderService.Setup(s => s.Save());

            var notFoundResult = this._reminderController.DeleteReminder(It.IsAny<int>()) as NotFoundResult;

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void DeleteReminder_WhenCalled_ReturnsOkResult()
        {
            this._mockReminderService.Setup(s => s.DeleteReminder(It.IsAny<Reminder>()));
            this._mockReminderService.Setup(s => s.GetReminderById(It.IsAny<int>()))
                .Returns(this.CreateMockReminder(1));
            this._mockReminderService.Setup(s => s.Save());

            var okResult = this._reminderController.DeleteReminder(It.IsAny<int>()) as StatusCodeResult;

            this._mockReminderService.Verify(s => s.GetReminderById(It.IsAny<int>()), Times.Once);
            this._mockReminderService.Verify(s => s.DeleteReminder(It.IsAny<Reminder>()), Times.Once);
            this._mockReminderService.Verify(s => s.Save(), Times.Once);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public void DeleteReminder_WhenCalled_ReturnsStatusCode500()
        {
            this._mockReminderService.Setup(s => s.DeleteReminder(It.IsAny<Reminder>()))
                .Throws(new Exception());
            this._mockReminderService.Setup(s => s.GetReminderById(It.IsAny<int>()))
                .Returns(this.CreateMockReminder(1));
            this._mockReminderService.Setup(s => s.Save());

            var errorResult = this._reminderController.DeleteReminder(It.IsAny<int>()) as ObjectResult;

            this._mockReminderService.Verify(s => s.GetReminderById(It.IsAny<int>()), Times.Once);
            this._mockReminderService.Verify(s => s.DeleteReminder(It.IsAny<Reminder>()), Times.Once);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
        #endregion

        #region Mock
        private Reminder CreateMockReminder(int? id = null, string code = "code", string label = "label")
        {
            return new Reminder()
            {
                Id = id,
                BrandId = 1,
                ClothesSizeId = 2,
                UserId = 3,
                Comments = "Comments"
            };
        }
        #endregion
    }
}
