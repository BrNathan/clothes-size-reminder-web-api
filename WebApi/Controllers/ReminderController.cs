using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Entities.ExtendedModels;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace WebApi.Controllers
{
    [Route("api/reminder")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IReminderService _reminderService;
        private readonly IClothesSizeService _clothesSizeService;

        public ReminderController(IReminderService reminderService, IClothesSizeService clothesSizeService)
        {
            _reminderService = reminderService;
            _clothesSizeService = clothesSizeService;
        }

        [HttpGet]
        public IActionResult GetAllReminders()
        {
            try
            {
                IEnumerable<Reminder> reminders = _reminderService.GetAllReminders();
                return Ok(reminders);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/reminder/GetAllReminders");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [Route("extend/user")]
        [HttpGet]
        public IActionResult GetAllRemindersExtendByUser()
        {
            try
            {
                int userId = 1;
                IEnumerable<Reminder> reminders = _reminderService.GetAllRemindersByUser(userId).ToList();
                IEnumerable<ClothesSize> clothesSizes = _clothesSizeService.GetAllClothesSizesByIds(reminders.Select(r => r.ClothesSizeId)).ToList();
                IEnumerable<ReminderExtended> result = reminders.Join(
                    clothesSizes,
                    r => r.ClothesSizeId,
                    s => s.Id,
                    (reminder, clothesSizes2) =>
                    {
                        return new ReminderExtended()
                        {
                            Id = reminder.Id,
                            UserId = reminder.UserId,
                            BrandId = reminder.BrandId,
                            ClothesSize = clothesSizes2,
                            Comments = reminder.Comments
                        };
                    }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/reminder/GetAllRemindersExtendByUser");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ReminderById")]
        public IActionResult GetReminderById(int id)
        {
            try
            {
                Reminder reminder = _reminderService.GetReminderById(id);
                if (reminder == null)
                {
                    _logger.Error($"Reminder with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(reminder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/reminder/GetReminderById/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateReminder([FromBody]Reminder reminder)
        {
            try
            {
                if (reminder.IsEntityNull())
                {
                    return BadRequest("Reminder object is null");
                }

                if (!reminder.IsEntityEmpty())
                {
                    return BadRequest("For create, the Id must be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _reminderService.CreateReminder(reminder);
                _reminderService.Save();

                return CreatedAtRoute("ReminderById", new { id = reminder.Id.Value }, reminder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/reminder/CreateReminder", reminder);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReminder(int id, [FromBody]Reminder reminder)
        {
            try
            {
                if (reminder.IsEntityNull())
                {
                    return BadRequest("Reminder object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                Reminder dbReminder = _reminderService.GetReminderById(id);
                if (dbReminder.IsEntityNull())
                {
                    _logger.Error($"Reminder with id: {id} not found in db");
                    return NotFound();
                }

                _reminderService.UpdateReminder(dbReminder, reminder);
                _reminderService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/reminder/UpdateReminder/" + id, reminder);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReminder(int id)
        {
            try
            {
                Reminder reminder = _reminderService.GetReminderById(id);
                if (reminder.IsEntityNull())
                {
                    _logger.Error($"Reminder with id: {id} not found in db");
                    return NotFound();
                }

                _reminderService.DeleteReminder(reminder);
                _reminderService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/reminder/DeleteReminder/" + id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}