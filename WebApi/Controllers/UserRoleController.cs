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
    [Route("api/userRole")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public IActionResult GetAllUserRoles()
        {
            try
            {
                IEnumerable<UserRole> userRoles = _userRoleService.GetAllUserRoles();
                return Ok(userRoles);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/userRole/GetAllUserRoles");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "UserRoleById")]
        public IActionResult GetUserRoleById(int id)
        {
            try
            {
                UserRole userRole = _userRoleService.GetUserRoleById(id);
                if (userRole == null)
                {
                    _logger.Error($"UserRole with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(userRole);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/userRole/GetUserRoleById/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateUserRole([FromBody]UserRole userRole)
        {
            try
            {
                if (userRole.IsEntityNull())
                {
                    return BadRequest("UserRole object is null");
                }

                if (!userRole.IsEntityEmpty())
                {
                    return BadRequest("For create, the Id must be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _userRoleService.CreateUserRole(userRole);
                _userRoleService.Save();

                return CreatedAtRoute("UserRoleById", new { id = userRole.Id.Value }, userRole);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/userRole/CreateUserRole", userRole);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserRole(int id, [FromBody]UserRole userRole)
        {
            try
            {
                if (userRole.IsEntityNull())
                {
                    return BadRequest("UserRole object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                UserRole dbUserRole = _userRoleService.GetUserRoleById(id);
                if (dbUserRole.IsEntityNull())
                {
                    _logger.Error($"UserRole with id: {id} not found in db");
                    return NotFound();
                }

                _userRoleService.UpdateUserRole(dbUserRole, userRole);
                _userRoleService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/userRole/UpdateUserRole/" + id, userRole);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserRole(int id)
        {
            try
            {
                UserRole userRole = _userRoleService.GetUserRoleById(id);
                if (userRole.IsEntityNull())
                {
                    _logger.Error($"UserRole with id: {id} not found in db");
                    return NotFound();
                }

                _userRoleService.DeleteUserRole(userRole);
                _userRoleService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/userRole/DeleteUserRole/" + id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}