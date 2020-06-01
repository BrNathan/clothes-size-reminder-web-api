using BLL.Interfaces;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/user/GetAllUsers");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "UserById")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                User user = _userService.GetUserById(id);
                if (user == null)
                {
                    _logger.Error($"User with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/user/GetUserById/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                if (user.IsEntityNull())
                {
                    return BadRequest("User object is null");
                }

                if (!user.IsEntityEmpty())
                {
                    return BadRequest("For create, the Id must be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _userService.CreateUser(user);
                _userService.Save();

                return CreatedAtRoute("UserById", new { id = user.Id.Value }, user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/user/CreateUser", user);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (user.IsEntityNull())
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                User dbUser = _userService.GetUserById(id);
                if (dbUser.IsEntityNull())
                {
                    _logger.Error($"User with id: {id} not found in db");
                    return NotFound();
                }

                _userService.UpdateUser(dbUser, user);
                _userService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/user/UpdateUser/" + id, user);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                User user = _userService.GetUserById(id);
                if (user.IsEntityNull())
                {
                    _logger.Error($"User with id: {id} not found in db");
                    return NotFound();
                }

                _userService.DeleteUser(user);
                _userService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/user/DeleteUser/" + id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}