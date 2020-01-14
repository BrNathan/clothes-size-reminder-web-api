using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
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

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
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

        [HttpPut]
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