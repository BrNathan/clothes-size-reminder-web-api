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
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            try
            {
                IEnumerable<Role> roles = _roleService.GetAllRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/role/GetAllRoles");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "RoleById")]
        public IActionResult GetRoleById(int id)
        {
            try
            {
                Role role = _roleService.GetRoleById(id);
                if (role == null)
                {
                    _logger.Error($"Role with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/role/GetRoleById/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateRole([FromBody]Role role)
        {
            try
            {
                if (role.IsEntityNull())
                {
                    return BadRequest("Role object is null");
                }

                if (!role.IsEntityEmpty())
                {
                    return BadRequest("For create, the Id must be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _roleService.CreateRole(role);
                _roleService.Save();

                return CreatedAtRoute("RoleById", new { id = role.Id.Value }, role);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/role/CreateRole", role);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody]Role role)
        {
            try
            {
                if (role.IsEntityNull())
                {
                    return BadRequest("Role object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                Role dbRole = _roleService.GetRoleById(id);
                if (dbRole.IsEntityNull())
                {
                    _logger.Error($"Role with id: {id} not found in db");
                    return NotFound();
                }

                _roleService.UpdateRole(dbRole, role);
                _roleService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/role/UpdateRole/" + id, role);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                Role role = _roleService.GetRoleById(id);
                if (role.IsEntityNull())
                {
                    _logger.Error($"Role with id: {id} not found in db");
                    return NotFound();
                }

                _roleService.DeleteRole(role);
                _roleService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/role/DeleteRole/" + id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}